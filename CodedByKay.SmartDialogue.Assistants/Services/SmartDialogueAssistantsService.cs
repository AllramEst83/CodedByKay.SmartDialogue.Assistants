
using CodedByKay.SmartDialogue.Assistants.Interfaces;
using CodedByKay.SmartDialogue.Assistants.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading;

namespace CodedByKay.CodedByKay.SmartDialogue.Assistants.Services
{
    /// <summary>
    /// Service to handle sending messages to OpenAI and managing chat history.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the SmartDialogueService with necessary dependencies.
    /// </remarks>
    /// <param name="httpClient">HttpClient used for making API requests.</param>
    /// <param name="options">Configuration options for the service.</param>
    /// <param name="chatHistoryService">Service for managing chat history.</param>
    /// <exception cref="ArgumentNullException">Thrown if httpClient is null.</exception>
    public class SmartDialogueAssistantsService(HttpClient httpClient, SmartDialogueOptions options, IChatHistoryService chatHistoryService) : ISmartDialogueAssistantsService
    {
        private readonly SmartDialogueOptions _options = options;
        private readonly IChatHistoryService _chatHistoryService = chatHistoryService;
        private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        /// <summary>
        /// Sends a chat message asynchronously, logs it, and gets a response from an AI model.
        /// </summary>
        /// <param name="chatId">The chat identifier.</param>
        /// <param name="message">The message to send.</param>
        /// <param name="messageType">The type of the message.</param>
        /// <returns>The model's response to the chat message.</returns>
        public async Task<string> SendChatMessageAsync(Guid chatId, string message, MessageType messageType)
        {
            // Log the user's message to the chat history
            _chatHistoryService.AddChatMessage(message, chatId, MessageType.User);

            // Create request data for the assistant model
            var requestData = CreateAssistantRequestData(message);
            var data = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            // Post the thread run and wait for completion
            var threadRunResponse = await PostThreadRunAsync(data);
            await WaitForThreadRunCompletion(threadRunResponse);

            // Retrieve the model's answer and process it
            var modelAnswer = await RetrieveModelAnswer(threadRunResponse.ThreadId);
            ProcessModelAnswer(modelAnswer, chatId);

            return modelAnswer;
        }

        /// <summary>
        /// Creates the assistant request data model for the given message.
        /// </summary>
        /// <param name="message">The message to send to the assistant.</param>
        /// <returns>The request data model for the assistant.</returns>
        private AssistantModel CreateAssistantRequestData(string message)
        {
            return new AssistantModel
            {
                AssistantId = _options.OpenAIAssistantId,
                Thread = new AssistantThread
                {
                    ThreadMessages = new List<ThreadMessage>
                    {
                        new() {Role = MessageType.User.ToString().ToLower(), Content = message}
                    }
                }
            };
        }

        /// <summary>
        /// Posts a thread run to the assistant and returns the initial response.
        /// </summary>
        /// <param name="data">The serialized request data for the assistant.</param>
        /// <returns>The initial thread run response from the assistant.</returns>
        private async Task<CreateThreadRunResponse> PostThreadRunAsync(StringContent data)
        {
            var response = await _httpClient.PostAsync("threads/runs", data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var threadRunResponse = JsonConvert.DeserializeObject<CreateThreadRunResponse>(responseBody);

            if (threadRunResponse == null)
            {
                throw new Exception("threadRunResponse is null.");
            }

            return threadRunResponse;
        }

        /// <summary>
        /// Waits for the thread run to complete by polling the assistant's API.
        /// </summary>
        /// <param name="threadRunResponse">The initial thread run response.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task WaitForThreadRunCompletion(CreateThreadRunResponse threadRunResponse)
        {
            bool isCompleted = false;
            while (!isCompleted)
            {
                var runResponse = await _httpClient.GetAsync($"threads/{threadRunResponse.ThreadId}/runs/{threadRunResponse.Id}").ConfigureAwait(false);
                var runResponseBody = await runResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var run = JsonConvert.DeserializeObject<RetriveThreadRunResponse>(runResponseBody);

                if (run != null && run.Status == "completed")
                {
                    isCompleted = true;
                }
                else
                {
                    await Task.Delay(1000);
                }
            }
        }

        /// <summary>
        /// Retrieves the model answer from the assistant after the thread run completes.
        /// </summary>
        /// <param name="threadId">The thread identifier for the assistant interaction.</param>
        /// <returns>The model answer as a string.</returns>
        private async Task<string> RetrieveModelAnswer(string threadId)
        {
            var messageResponse = await _httpClient.GetAsync($"threads/{threadId}/messages").ConfigureAwait(false);
            messageResponse.EnsureSuccessStatusCode();

            var messageResponseBody = await messageResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var messages = JsonConvert.DeserializeObject<ThreadMessageListResponse>(messageResponseBody);

            if (messages == null)
            {
                throw new Exception("ThreadMessageListResponse data is empty.");
            }

            return messages.Data
                .Where(m => m.Role == MessageType.User.ToString().ToLower())
                .SelectMany(m => m.Content)
                .Aggregate(string.Empty, (current, item) => current + item.Text.Value);
        }

        /// <summary>
        /// Processes the model's answer by logging it and recalculating the chat history length.
        /// </summary>
        /// <param name="modelAnswer">The model's answer to the chat message.</param>
        /// <param name="chatId">The chat identifier.</param>
        private void ProcessModelAnswer(string modelAnswer, Guid chatId)
        {
            if (!string.IsNullOrEmpty(modelAnswer))
            {
                // Log the model's answer and recalculate the chat history length
                _chatHistoryService.AddChatMessage(modelAnswer, chatId, MessageType.Assistant);
                _chatHistoryService.ReCalculateHistoryLength(chatId, _options.MaxTokens);
            }
            else
            {
                throw new Exception("The response from OpenAI did not contain a valid 'modelAnswer'.");
            }
        }


        /// <summary>
        /// Deletes the chat history associated with a specific chat ID.
        /// </summary>
        /// <param name="chatId">The unique identifier of the chat to be deleted.</param>
        /// <returns>True if the chat history was successfully removed; otherwise, false.</returns>
        /// <remarks>This method attempts to remove the chat history for the specified chatId. If the chatId does not exist or has already been deleted, the method will return false.</remarks>
        public bool DeleteChatById(Guid chatId)
        {
            // Attempt to delete the chat history by the given chatId.
            var isRemoved = _chatHistoryService.DeleteChatHistoryById(chatId);

            // Return the result of the deletion attempt.
            return isRemoved;
        }

        /// <summary>
        /// Deletes all chat histories managed by the service.
        /// </summary>
        /// <returns>Always returns true to indicate the operation was requested.</returns>
        /// <remarks>This method invokes the chat history service to clear all stored chat histories. It always returns true as an indication that the operation was performed, without specifying whether any chat histories were actually present and deleted.</remarks>
        public bool DeleteAllChats()
        {
            // Request the chat history service to delete all chat histories.
            _chatHistoryService.DeleteAllChatHistories();

            // Return true to indicate the delete operation was executed.
            return true;
        }

    }
}

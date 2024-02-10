namespace CodedByKay.SmartDialogue.Assistants.Models
{
    /// <summary>
    /// Configuration options for CodedByKay.SmartDialogue.Assistants, including API settings and default values for interacting with OpenAI.
    /// </summary>
    public class SmartDialogueOptions
    {
        /// <summary>
        /// Gets or sets the OpenAI API URL. Default is "https://api.openai.com/v1/".
        /// </summary>
        public string OpenAIApiUrl { get; set; } = "https://api.openai.com/v1/";

        /// <summary>
        /// Gets or sets the OpenAI API key. Replace "open_ai_api_key" with your actual API key.
        /// </summary>
        public string OpenAIApiKey { get; set; } = "open_ai_api_key";
        /// <summary>
        /// Gets or sets the OpenAI API assistant id. Replace "assistant_id" with your actual assitant id.
        /// </summary>
        public string OpenAIAssistantId { get; set; } = "assistant_id";

        /// <summary>
        /// Gets or sets the maximum number of tokens to generate in the completion. Default is 2000.
        /// </summary>
        public int MaxTokens { get; set; } = 2000;

        /// <summary>
        /// Gets or sets the average token length used for estimating token count. Default is 2.85.
        /// </summary>
        /// <remarks>
        /// This value is used to estimate the number of tokens in the input text based on character count.
        /// </remarks>
        public double AverageTokenLength { get; set; } = 2.85;
    }
}

using Newtonsoft.Json;

namespace CodedByKay.SmartDialogue.Assistants.Models
{
    internal class AssistantModel
    {
        [JsonProperty("assistant_id")]
        public string AssistantId { get; set; } = "";

        [JsonProperty("thread")]
        public AssistantThread AssistantThread { get; set; } = new AssistantThread();
    }

    internal class AssistantThread
    {
        [JsonProperty("messages")]
        public List<ThreadMessage> ThreadMessages { get; set; } = [];
    }

    internal class ThreadMessage
    {
        [JsonProperty("role")]
        public string Role { get; set; } = "";

        [JsonProperty("content")]
        public string Content { get; set; } = "";
    }

}

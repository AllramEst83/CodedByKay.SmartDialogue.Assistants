using Newtonsoft.Json;

namespace CodedByKay.SmartDialogue.Assistants.Models
{
    internal class Message
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("thread_id")]
        public string ThreadId { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("content")]
        public List<ContentItem> Content { get; set; }

        [JsonProperty("file_ids")]
        public List<string> FileIds { get; set; }

        [JsonProperty("assistant_id")]
        public string AssistantId { get; set; }

        [JsonProperty("run_id")]
        public string RunId { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }
}

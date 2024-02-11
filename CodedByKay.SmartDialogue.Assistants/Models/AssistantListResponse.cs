using Newtonsoft.Json;

namespace CodedByKay.SmartDialogue.Assistants.Models
{
    public class AssistantListResponse
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("data")]
        public Assistant[] Data { get; set; }

        [JsonProperty("first_id")]
        public string FirstId { get; set; }

        [JsonProperty("last_id")]
        public string LastId { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }
    }

    public class Assistant
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("instructions")]
        public string Instructions { get; set; }

        [JsonProperty("tools")]
        public object[] Tools { get; set; }

        [JsonProperty("file_ids")]
        public object[] FileIds { get; set; }

        [JsonProperty("metadata")]
        public object Metadata { get; set; }
    }
}

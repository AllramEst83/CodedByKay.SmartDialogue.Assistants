using Newtonsoft.Json;

namespace CodedByKay.SmartDialogue.Assistants.Models
{
    internal class ThreadMessageListResponse
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("data")]
        public List<Message> Data { get; set; }

        [JsonProperty("first_id")]
        public string FirstId { get; set; }

        [JsonProperty("last_id")]
        public string LastId { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }
    }
}

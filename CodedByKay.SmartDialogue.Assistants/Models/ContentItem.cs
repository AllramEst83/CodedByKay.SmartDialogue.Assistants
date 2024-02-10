using Newtonsoft.Json;

namespace CodedByKay.SmartDialogue.Assistants.Models
{
    internal class ContentItem
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("text")]
        public TextContent Text { get; set; }
    }
}

using Newtonsoft.Json;

namespace CodedByKay.SmartDialogue.Assistants.Models
{
    internal class TextContent
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("annotations")]
        public List<object> Annotations { get; set; }
    }
}

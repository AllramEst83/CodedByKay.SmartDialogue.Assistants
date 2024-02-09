using Newtonsoft.Json;

namespace CodedByKay.SmartDialogue.Assistants.Models
{
    internal class Tool
    {
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }
}

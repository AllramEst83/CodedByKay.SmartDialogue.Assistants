using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

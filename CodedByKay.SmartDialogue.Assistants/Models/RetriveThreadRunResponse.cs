using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedByKay.SmartDialogue.Assistants.Models
{
    internal class RetriveThreadRunResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("assistant_id")]
        public string AssistantId { get; set; }

        [JsonProperty("thread_id")]
        public string ThreadId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("started_at")]
        public long? StartedAt { get; set; }

        [JsonProperty("expires_at")]
        public long? ExpiresAt { get; set; }

        [JsonProperty("cancelled_at")]
        public object CancelledAt { get; set; }

        [JsonProperty("failed_at")]
        public object FailedAt { get; set; }

        [JsonProperty("completed_at")]
        public long? CompletedAt { get; set; }

        [JsonProperty("last_error")]
        public object LastError { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("instructions")]
        public string Instructions { get; set; }

        [JsonProperty("tools")]
        public List<Tool> Tools { get; set; }

        [JsonProperty("file_ids")]
        public List<string> FileIds { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("usage")]
        public Usage Usage { get; set; }
    }
}

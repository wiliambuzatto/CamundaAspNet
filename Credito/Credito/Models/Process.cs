using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Credito.Models
{
    public class Process
    {
        [JsonProperty("variables")]
        public IDictionary<string, object> Variables { get; set; }

        [JsonProperty("businessKey")]
        public string BusinessKey { get; set; }
    }
}

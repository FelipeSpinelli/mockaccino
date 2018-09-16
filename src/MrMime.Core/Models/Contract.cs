using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MrMime.Core.Models
{
    public class Contract
    {
        [JsonProperty(PropertyName = "contract_id")]
        public Guid ContractId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "fields")]
        public IList<ContractField> Fields { get; set; }
    }
}

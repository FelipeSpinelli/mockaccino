using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MrMime.Core.Models
{
    internal class Contract
    {
        [JsonProperty(PropertyName = "contract_id")]
        internal Guid ContractId { get; set; }

        [JsonProperty(PropertyName = "name")]
        internal string Name { get; set; }

        [JsonProperty(PropertyName = "fields")]
        internal IList<ContractField> Fields { get; set; }
    }
}

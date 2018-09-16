using Newtonsoft.Json;

namespace MrMime.Core.Models
{
    internal class ContractField
    {
        [JsonProperty(PropertyName = "name")]
        internal string Name { get; set; }

        [JsonProperty(PropertyName = "type")]
        internal FieldTypeEnum Type { get; set; }

        [JsonProperty(PropertyName = "is_nullable")]
        internal bool IsNullable { get; set; }

        [JsonProperty(PropertyName = "fill_mode")]
        internal FieldFillModeEnum FillMode { get; set; }

        [JsonProperty(PropertyName = "default_value")]
        internal object DefaultValue { get; set; }

        [JsonProperty(PropertyName = "min_value")]
        internal int? MinValue { get; set; }

        [JsonProperty(PropertyName = "max_value")]
        internal int? MaxValue { get; set; }

        [JsonProperty(PropertyName = "regex_pattern")]
        internal string RegexPattern { get; set; }
    }
}
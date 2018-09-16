using Newtonsoft.Json;

namespace MrMime.Core.Models
{
    public class ContractField
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "type")]
        public FieldTypeEnum Type { get; set; }

        [JsonProperty(PropertyName = "is_nullable")]
        public bool IsNullable { get; set; }

        [JsonProperty(PropertyName = "fill_mode")]
        public FieldFillModeEnum FillMode { get; set; }

        [JsonProperty(PropertyName = "default_value")]
        public object DefaultValue { get; set; }

        [JsonProperty(PropertyName = "min_value")]
        public int? MinValue { get; set; }

        [JsonProperty(PropertyName = "max_value")]
        public int? MaxValue { get; set; }

        [JsonProperty(PropertyName = "regex_pattern")]
        public string RegexPattern { get; set; }
    }
}
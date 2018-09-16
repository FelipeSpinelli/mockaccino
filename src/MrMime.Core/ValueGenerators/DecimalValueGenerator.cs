using MrMime.Core.Models;
using System;

namespace MrMime.Core.ValueGenerators
{
    internal class DecimalValueGenerator : ValueGenerator
    {
        private DecimalValueGenerator() { }

        protected override object GenerateValue(ContractField field)
        {
            switch (field.FillMode)
            {
                case FieldFillModeEnum.None:
                    return 0.00m;
                case FieldFillModeEnum.Null:
                    return (decimal?)null;
                case FieldFillModeEnum.Random:
                    var random = new Random();
                    var randomNull = random.Next(field.IsNullable ? 0 : 1, 1);
                    return (decimal?)randomNull == 0 ? (decimal?)null : Convert.ToDecimal(random.NextDouble() * 100.00d);
                case FieldFillModeEnum.Fixed:
                    return decimal.Parse(field.DefaultValue.ToString());
                default:
                    return (decimal?)null;
            }
        }

        public static DecimalValueGenerator _instance = new DecimalValueGenerator();
        public static decimal? GetValue(ContractField field)
        {
            return (decimal?)_instance.GenerateValue(field);
        }
    }
}

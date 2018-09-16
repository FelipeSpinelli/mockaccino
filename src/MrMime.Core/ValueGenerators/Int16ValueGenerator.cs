using MrMime.Core.Models;
using System;

namespace MrMime.Core.ValueGenerators
{
    internal class Int16ValueGenerator : ValueGenerator
    {
        private Int16ValueGenerator() { }

        protected override object GenerateValue(ContractField field)
        {
            switch (field.FillMode)
            {
                case FieldFillModeEnum.None:
                    return 0;
                case FieldFillModeEnum.Null:
                    return (short?)null;
                case FieldFillModeEnum.Random:
                    var random = new Random();
                    var randomNull = random.Next(field.IsNullable ? 0 : 1, 1);
                    return (short?)randomNull == 0 ? (short?)null : (short?)random.Next(field.MinValue ?? short.MinValue, field.MaxValue ?? short.MaxValue);
                case FieldFillModeEnum.Fixed:
                    return short.Parse(field.DefaultValue.ToString());
                default:
                    return (short?)null;
            }
        }

        public static Int16ValueGenerator _instance = new Int16ValueGenerator();
        public static short? GetValue(ContractField field)
        {
            return (short?)_instance.GenerateValue(field);
        }
    }
}

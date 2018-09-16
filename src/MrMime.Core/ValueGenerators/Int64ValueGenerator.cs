using MrMime.Core.Models;
using System;

namespace MrMime.Core.ValueGenerators
{
    internal class Int64ValueGenerator : ValueGenerator
    {
        private Int64ValueGenerator() { }

        protected override object GenerateValue(ContractField field)
        {
            switch (field.FillMode)
            {
                case FieldFillModeEnum.None:
                    return 0;
                case FieldFillModeEnum.Null:
                    return (long?)null;
                case FieldFillModeEnum.Random:
                    var random = new Random();
                    var randomNull = random.Next(field.IsNullable ? 0 : 1, 1);
                    return (long?)randomNull == 0 ? (long?)null : random.Next(field.MinValue ?? int.MinValue, field.MaxValue ?? int.MaxValue);
                case FieldFillModeEnum.Fixed:
                    return long.Parse(field.DefaultValue.ToString());
                default:
                    return (long?)null;
            }
        }

        internal static Int64ValueGenerator _instance = new Int64ValueGenerator();
        internal static long? GetValue(ContractField field)
        {
            return (long?)_instance.GenerateValue(field);
        }
    }
}

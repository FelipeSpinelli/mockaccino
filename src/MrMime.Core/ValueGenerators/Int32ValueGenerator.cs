using MrMime.Core.Models;
using System;

namespace MrMime.Core.ValueGenerators
{
    internal class Int32ValueGenerator : ValueGenerator
    {
        private Int32ValueGenerator() { }

        protected override object GenerateValue(ContractField field)
        {
            switch (field.FillMode)
            {
                case FieldFillModeEnum.None:
                    return 0;
                case FieldFillModeEnum.Null:
                    return (int?)null;
                case FieldFillModeEnum.Random:
                    var random = new Random();
                    var randomNull = random.Next(field.IsNullable ? 0 : 1, 1);
                    return (int?)randomNull == 0 ? (int?)null : random.Next(field.MinValue ?? int.MinValue, field.MaxValue ?? int.MaxValue);
                case FieldFillModeEnum.Fixed:
                    return int.Parse(field.DefaultValue.ToString());
                default:
                    return (int?)null;
            }
        }

        internal static Int32ValueGenerator _instance = new Int32ValueGenerator();
        internal static int? GetValue(ContractField field)
        {
            return (int?)_instance.GenerateValue(field);
        }
    }
}

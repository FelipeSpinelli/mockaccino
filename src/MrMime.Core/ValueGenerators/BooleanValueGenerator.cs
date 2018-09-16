using MrMime.Core.Models;
using System;

namespace MrMime.Core.ValueGenerators
{
    internal class BooleanValueGenerator : ValueGenerator
    {
        private BooleanValueGenerator() { }

        protected override object GenerateValue(ContractField field)
        {
            switch (field.FillMode)
            {
                case FieldFillModeEnum.None:
                    return false;
                case FieldFillModeEnum.Null:
                    return (bool?)null;
                case FieldFillModeEnum.Random:
                    var random = new Random().Next(field.IsNullable ? -1 : 0, 1);
                    return random == -1? (bool?)null : random == 0? (bool?)false : (bool?)true;
                case FieldFillModeEnum.Fixed:
                    return bool.Parse(field.DefaultValue.ToString());
                default:
                    return (bool?)null;
            }
        }

        public static BooleanValueGenerator _instance = new BooleanValueGenerator();
        public static bool? GetValue(ContractField field)
        {
            return (bool?)_instance.GenerateValue(field);
        }
    }
}

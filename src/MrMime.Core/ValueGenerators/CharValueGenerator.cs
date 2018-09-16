using MrMime.Core.Models;
using System;

namespace MrMime.Core.ValueGenerators
{
    internal class CharValueGenerator : ValueGenerator
    {
        private CharValueGenerator() { }

        protected override object GenerateValue(ContractField field)
        {
            switch (field.FillMode)
            {
                case FieldFillModeEnum.None:
                    return (char?)0;
                case FieldFillModeEnum.Null:
                    return (char?)null;
                case FieldFillModeEnum.Random:
                    var random = new Random().Next(field.IsNullable ? -1 : 0, 255);
                    return random == -1 ? (char?)null : (char?)random;
                case FieldFillModeEnum.Fixed:
                    return Convert.ToChar(field.DefaultValue);
                default:
                    return (char?)null;
            }
        }

        public static CharValueGenerator _instance = new CharValueGenerator();
        public static char? GetValue(ContractField field)
        {
            return (char?)_instance.GenerateValue(field);
        }
    }
}

using MrMime.Core.Models;
using System;

namespace MrMime.Core.ValueGenerators
{
    internal class GuidValueGenerator : ValueGenerator
    {
        private GuidValueGenerator() { }

        protected override object GenerateValue(ContractField field)
        {
            switch (field.FillMode)
            {
                case FieldFillModeEnum.None:
                    return new Guid();
                case FieldFillModeEnum.Null:
                    return (Guid?)null;
                case FieldFillModeEnum.Random:
                    var random = new Random().Next(field.IsNullable ? 0 : 1, 1);
                    return random == 0 ? (Guid?)null : (Guid?)Guid.NewGuid();
                case FieldFillModeEnum.Fixed:
                    return Guid.Parse(field.DefaultValue.ToString());
                default:
                    return (Guid?)null;
            }
        }

        public static GuidValueGenerator _instance = new GuidValueGenerator();
        public static Guid? GetValue(ContractField field)
        {
            return (Guid?)_instance.GenerateValue(field);
        }
    }
}

using MrMime.Core.Models;
using System;

namespace MrMime.Core.ValueGenerators
{
    internal class DateTimeValueGenerator : ValueGenerator
    {
        private DateTimeValueGenerator() { }

        protected override object GenerateValue(ContractField field)
        {
            switch (field.FillMode)
            {
                case FieldFillModeEnum.None:
                    return new DateTime();
                case FieldFillModeEnum.Null:
                    return (DateTime?)null;
                case FieldFillModeEnum.Random:
                    var random = new Random();
                    var randomNull = random.Next(field.IsNullable ? 0 : 1, 1);
                    var randomYear = random.Next(1970, DateTime.Now.Year);
                    var randomMonth = random.Next(1, 12);
                    var randomDay = random.Next(1, 28);
                    return randomNull == 0 ? (DateTime?)null : (DateTime?)new DateTime(randomYear, randomMonth, randomDay);
                case FieldFillModeEnum.Fixed:
                    return DateTime.Parse(field.DefaultValue.ToString());
                default:
                    return (DateTime?)null;
            }
        }

        internal static DateTimeValueGenerator _instance = new DateTimeValueGenerator();
        internal static DateTime? GetValue(ContractField field)
        {
            return (DateTime?)_instance.GenerateValue(field);
        }
    }
}

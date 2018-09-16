using MrMime.Core.Models;
using System.Diagnostics;

namespace MrMime.Core.ValueGenerators
{

    public abstract class ValueGenerator
    {
        protected abstract object GenerateValue(ContractField field);

        public static object GetValue(ContractField field)
        {
            object value = null;
            switch (field.Type)
            {
                case FieldTypeEnum.String:
                    value = StringValueGenerator.GetValue(field);
                    break;
                case FieldTypeEnum.Int16:
                    value = Int16ValueGenerator.GetValue(field);
                    break;
                case FieldTypeEnum.Int32:
                    value = Int32ValueGenerator.GetValue(field);
                    break;
                case FieldTypeEnum.Int64:
                    value = Int64ValueGenerator.GetValue(field);
                    break;
                case FieldTypeEnum.Boolean:
                    value = BooleanValueGenerator.GetValue(field);
                    break;
                case FieldTypeEnum.DateTime:
                    value = DateTimeValueGenerator.GetValue(field);
                    break;
                case FieldTypeEnum.Char:
                    value = CharValueGenerator.GetValue(field);
                    break;
                case FieldTypeEnum.Guid:
                    value = GuidValueGenerator.GetValue(field);
                    break;
                default:
                    value = null;
                    break;
            }
            Debug.WriteLine($"Generated value: {value}");
            return value;
        }
    }
}

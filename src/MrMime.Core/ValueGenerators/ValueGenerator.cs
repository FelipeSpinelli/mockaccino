using MrMime.Core.Models;

namespace MrMime.Core.ValueGenerators
{

    internal abstract class ValueGenerator
    {
        protected abstract object GenerateValue(ContractField field);

        internal static object GetValue(ContractField field)
        {
            switch (field.Type)
            {
                case FieldTypeEnum.String:
                    return StringValueGenerator.GetValue(field);
                case FieldTypeEnum.Int16:
                    return Int16ValueGenerator.GetValue(field);
                case FieldTypeEnum.Int32:
                    return Int32ValueGenerator.GetValue(field);
                case FieldTypeEnum.Int64:
                    return Int64ValueGenerator.GetValue(field);
                case FieldTypeEnum.Boolean:
                    return BooleanValueGenerator.GetValue(field);
                case FieldTypeEnum.DateTime:
                    return DateTimeValueGenerator.GetValue(field);
                case FieldTypeEnum.Char:
                    return CharValueGenerator.GetValue(field);
                case FieldTypeEnum.Guid:
                    return GuidValueGenerator.GetValue(field);
                default:
                    return null;
            }
        }
    }
}

using MrMime.Core.Models;
using System;

namespace MrMime.Core.ValueGenerators
{
    internal class StringValueGenerator : ValueGenerator
    {
        private StringValueGenerator() { }
        private readonly string[] LOREM_IPSUM_WORDS = new string[]
        {
            "Lorem", "ipsum",
            "dolor", "sit",
            "amet", "consectetur",
            "adipiscing", "elit",
            "Phasellus", "et",
            "ipsum", "fermentum",
            "laoreet", "risus",
            "ac", "dapibus",
            "est", "Aliquam",
            "erat", "volutpat"
        };

        protected override object GenerateValue(ContractField field)
        {
            switch (field.FillMode)
            {
                case FieldFillModeEnum.None:
                    return string.Empty;
                case FieldFillModeEnum.Null:
                    return null;
                case FieldFillModeEnum.Random:
                    var random = new Random();
                    var length = random.Next(field.MinValue ?? 0, field.MaxValue ?? 10);
                    var value = string.Empty;
                    while (value.Length < length)
                    {
                        var wordIndex = random.Next(0, LOREM_IPSUM_WORDS.Length);
                        value += LOREM_IPSUM_WORDS[wordIndex] + " ";
                    }
                    return value.TrimEnd().Substring(0, length);
                case FieldFillModeEnum.Fixed:
                    return field.DefaultValue.ToString();
                default:
                    return null;
            }
        }

        internal static StringValueGenerator _instance = new StringValueGenerator();
        internal static string GetValue(ContractField field)
        {
            return (string)_instance.GenerateValue(field);
        }
    }
}

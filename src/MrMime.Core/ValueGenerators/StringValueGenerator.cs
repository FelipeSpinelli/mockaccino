using MrMime.Core.Models;
using System;
using System.Diagnostics;

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
                    var minLength = field.MinValue ?? 1;
                    minLength = minLength < 1 ? 1 : minLength;

                    var maxLength = field.MaxValue ?? minLength + 10;
                    maxLength = maxLength < minLength ? minLength + 10 : maxLength;

                    var length = random.Next(minLength, maxLength);
                    var value = string.Empty;
                    while (value.Length < length)
                    {
                        var wordIndex = random.Next(0, LOREM_IPSUM_WORDS.Length);
                        value += LOREM_IPSUM_WORDS[wordIndex] + " ";
                    }

                    return value.Substring(0, length).Trim();
                case FieldFillModeEnum.Fixed:
                    return field.DefaultValue.ToString();
                default:
                    return null;
            }
        }

        public static StringValueGenerator _instance = new StringValueGenerator();
        public static string GetValue(ContractField field)
        {
            return (string)_instance.GenerateValue(field);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MrMime.Core.Models;
using MrMime.Core.ValueGenerators;

namespace MrMime.Core.Tests
{
    [TestClass]
    public class Tests_StringValueGenerator
    {
        [TestMethod]
        public void GetValue_None_Success()
        {
            var stringContractField = new ContractField
            {
                Name = "TestNone",
                Type = FieldTypeEnum.String,
                FillMode = FieldFillModeEnum.None                
            };

            var value = ValueGenerator.GetValue(stringContractField);
            Assert.AreEqual(string.Empty, value);
        }

        [TestMethod]
        public void GetValue_Null_Success()
        {
            var stringContractField = new ContractField
            {
                Name = "TestNull",
                Type = FieldTypeEnum.String,
                FillMode = FieldFillModeEnum.Null
            };

            var value = ValueGenerator.GetValue(stringContractField);
            Assert.AreEqual(null, value);
        }

        [TestMethod]
        public void GetValue_Fixed_Success()
        {
            var stringContractField = new ContractField
            {
                Name = "TestFixed",
                Type = FieldTypeEnum.String,
                FillMode = FieldFillModeEnum.Fixed,
                DefaultValue = "Fixed Value"
            };

            var value = ValueGenerator.GetValue(stringContractField);
            Assert.AreEqual("Fixed Value", value);
        }

        [TestMethod]
        public void GetValue_Random_Success()
        {
            var stringContractField = new ContractField
            {
                Name = "Test",
                Type = FieldTypeEnum.String,
                FillMode = FieldFillModeEnum.Random,
                MinValue = 5,
                MaxValue = 100
            };

            var value = ValueGenerator.GetValue(stringContractField).ToString();
            Assert.IsTrue(value.Length >= stringContractField.MinValue);
            Assert.IsTrue(value.Length <= stringContractField.MaxValue);
        }
    }
}

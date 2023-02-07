using FluentAssertions;

namespace Mockaccino.UnitTests
{
    public class MockControllerGenerationTests
    {
        [Theory]
        [InlineData("GetResourceById")]
        [InlineData("CreateResource")]
        [InlineData("PutResource")]
        [InlineData("PatchResource")]
        [InlineData("DeleteResource")]
        public void MockController_GivenMethodName_ShouldExistAsExpected(string expectedMethodName)
        {
            var mockControllerType = typeof(MockController);

            mockControllerType.GetMethod(expectedMethodName).Should().NotBeNull();
        }
    }
}
namespace Mockaccino;

internal static class MockExtensions
{
    internal static string GetConditionWith(this MockRequestFilter? filter, string returnSentence)
    {
        if (filter == null)
        {
            return returnSentence;
        }

        return filter.ToString().Replace($"{{{nameof(returnSentence)}}}", returnSentence);
    }
}
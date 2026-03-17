using CalastoneTest.Filters;
using FluentAssertions;

namespace CalastoneTest.UnitTests.Filters;

public class WordLessThan3FilterTests
{
    [Theory]
    [InlineData("a", true)]
    [InlineData("ab", true)]
    [InlineData("abc", false)]
    [InlineData("abcd", false)]
    [InlineData("", true)]
    public void ReturnsExpectedValue(string input, bool expected)
    {
        var filter = new WordLessThan3Filter();
        
        var result = filter.IsMatch(input);
        
        result.Should().Be(expected);
    }
}

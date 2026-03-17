using CalastoneTest.Filters;
using FluentAssertions;

namespace CalastoneTest.UnitTests.Filters;

public class VowelInTheMiddleFilterTests
{
    [Theory]
    [InlineData("", false)]
    [InlineData("b", false)]
    [InlineData("a", true)]
    [InlineData("bbb", false)]
    [InlineData("abc", false)]
    [InlineData("abec", true)]
    [InlineData("aabc", true)]
    [InlineData("aaaaabbbbbccccc", false)]
    [InlineData("bac", true)]
    [InlineData("cba", false)]
    [InlineData("B", false)]
    [InlineData("A", true)]
    [InlineData("BBB", false)]
    [InlineData("ABC", false)]
    [InlineData("ABEC", true)]
    [InlineData("AABC", true)]
    [InlineData("AAAAABBBBBCCCCC", false)]
    [InlineData("BAC", true)]
    [InlineData("CBA", false)]
    public void ReturnsExpectedValue(string input, bool expected)
    {
        var filter = new VowelInTheMiddleFilter();
        
        var result = filter.IsMatch(input);

        result.Should().Be(expected);
    }
}

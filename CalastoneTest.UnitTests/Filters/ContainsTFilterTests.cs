using CalastoneTest.Filters;
using FluentAssertions;

namespace CalastoneTest.UnitTests.Filters;

public class ContainsTFilterTests
{
    [Theory]
    [InlineData("T", true)]
    [InlineData("t", true)]
    [InlineData("Trust", true)]
    [InlineData("Rest", true)]
    [InlineData("Hello", false)]
    [InlineData("", false)]
    [InlineData("a", false)]
    public void ReturnsExpectedValue(string input, bool expected)
    {
        var filter = new ContainsTFilter();
        
        var result = filter.IsMatch(input);
        
        result.Should().Be(expected);
    }
}

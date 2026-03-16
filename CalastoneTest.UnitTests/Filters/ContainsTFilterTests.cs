using CalastoneTest.Filters;

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
        // Arrange
        var filter = new ContainsTFilter();
        // Act
        var result = filter.IsMatch(input);
        // Assert
        Assert.Equal(expected, result);
    }
}

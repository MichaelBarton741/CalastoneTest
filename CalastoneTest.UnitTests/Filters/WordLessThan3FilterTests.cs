using CalastoneTest.Filters;

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
        // Arrange
        var filter = new WordLessThan3Filter();
        // Act
        var result = filter.IsMatch(input);
        // Assert
        Assert.Equal(expected, result);
    }
}

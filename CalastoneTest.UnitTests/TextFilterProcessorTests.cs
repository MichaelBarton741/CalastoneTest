using CalastoneTest.Filters;
using FluentAssertions;
using Moq;

namespace CalastoneTest.UnitTests;

public class TextFilterProcessorTests
{
    [Theory]
    [InlineData("Hello World", "Hello World")]
    [InlineData("Test!?   %$£%^    ?Testing!    ~@:", "Test Testing")]
    [InlineData("Symbols!@#$%^&*() are ignored.", "Symbols are ignored")]
    [InlineData("", "")]
    public void Process_NoFilters_GetExpectedOutputs(string input, string expectedOutput)
    {
        // Arrange
        var processor = new TextFilterProcessor([]);
        // Act
        var result = processor.Process(input);
        // Assert
        result.Should().BeEquivalentTo(expectedOutput);
    }

    [Fact]
    public void Process_WithMockFilter_Returns_FilteredOutput()
    {
        // Arrange
        var mockFilter = new Mock<IFilter>();
        // This mock filter will only match words with an even number of characters
        mockFilter.Setup(f => f.IsMatch(It.IsAny<string>())).Returns((string text) => text.Length % 2 == 0);
        var processor = new TextFilterProcessor([mockFilter.Object]);
        // Act
        var result = processor.Process("A AB ABC ABCD ABCDE ABCDEF");
        // Assert
        result.Should().BeEquivalentTo("A ABC ABCDE");
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("The big, rather happy dog and she ran quickly to a clean book.", "happy and she quickly")]
    [InlineData("Currently, clean what the rather book is happy and good.", "happy and")]
    [InlineData("Oh dear! It's a very short test, but she saw it quickly.", "she quickly")]
    public void Process_AllFilters_GetExpectedOutputs(string input, string expectedOutput)
    {
        // Arrange
        var filters = new IFilter[]
        {
            new VowelInTheMiddleFilter(),
            new WordLessThan3Filter(),
            new ContainsTFilter(),
        };
        var processor = new TextFilterProcessor(filters);
        // Act
        var result = processor.Process(input);
        // Assert
        result.Should().BeEquivalentTo(expectedOutput);
    }

    [Fact]
    public void Process_Input_Returns_ExpectedOutput()
    {
        // Arrange
        var filters = new IFilter[]
        {
            new VowelInTheMiddleFilter(),
            new WordLessThan3Filter(),
            new ContainsTFilter(),
        };
        var processor = new TextFilterProcessor(filters);
        var input = File.ReadAllText("Input.txt");
        var expected = File.ReadAllText("ExpectedOutput.txt");
        // Act
        var result = processor.Process(input);
        // Assert
        Assert.Equal(expected, result);
    }
}

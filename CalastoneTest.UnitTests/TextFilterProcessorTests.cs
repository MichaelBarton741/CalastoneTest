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
        var processor = new TextFilterProcessor([]);
        
        var result = processor.Process(input);
        
        result.Should().Be(expectedOutput);
    }

    [Fact]
    public void Process_WithMockFilter_Returns_FilteredOutput()
    {
        var mockFilter = new Mock<IFilter>();
        mockFilter.Setup(f => f.IsMatch(It.IsAny<string>())).Returns((string text) => text.Length % 2 == 0);
        var processor = new TextFilterProcessor([mockFilter.Object]);
        
        var result = processor.Process("A AB ABC ABCD ABCDE ABCDEF");
        
        result.Should().Be("A ABC ABCDE");
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("The big, rather happy dog and she ran quickly to a clean book.", "happy and she quickly")]
    [InlineData("Currently, clean what the rather book is happy and good.", "happy and")]
    [InlineData("Oh dear! It's a very short test, but she saw it quickly.", "she quickly")]
    public void Process_AllFilters_GetExpectedOutputs(string input, string expectedOutput)
    {
        var filters = new IFilter[]
        {
            new VowelInTheMiddleFilter(),
            new WordLessThan3Filter(),
            new ContainsTFilter(),
        };
        var processor = new TextFilterProcessor(filters);
        
        var result = processor.Process(input);
        
        result.Should().Be(expectedOutput);
    }

    [Fact]
    public void Process_Input_Returns_ExpectedOutput()
    {
        var filters = new IFilter[]
        {
            new VowelInTheMiddleFilter(),
            new WordLessThan3Filter(),
            new ContainsTFilter(),
        };
        var processor = new TextFilterProcessor(filters);
        var input = File.ReadAllText("Input.txt");
        var expected = File.ReadAllText("ExpectedOutput.txt");
        
        var result = processor.Process(input);
        
        result.Should().Be(expected);
    }
}

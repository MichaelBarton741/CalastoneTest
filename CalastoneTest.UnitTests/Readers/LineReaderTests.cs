using CalastoneTest.Readers;

namespace CalastoneTest.UnitTests.Readers;

public class LineReaderTests
{
    [Theory]
    [ClassData(typeof(NextSegmentTestData))]
    public void GetNextSegment_ReturnsExpectedSegments(string input, LineSegment[] expectedSegments)
    {
        // Arrange
        var lineReader = new LineReader(input);
        var actualSegments = new List<LineSegment>();
        // Act
        var segment = lineReader.GetNextSegment();
        while (segment != null)
        {
            actualSegments.Add(segment);
            segment = lineReader.GetNextSegment();
        }

        //for (int i = 0; i < expectedSegments.Length; i++)
        //{
        //    var segment = lineReader.GetNextSegment();
        //    if (segment != null)
        //    {
        //        actualSegments.Add(segment);
        //    }
        //}
        // Assert
        Assert.Equal(expectedSegments.Length, actualSegments.Count);
        for (int i = 0; i < expectedSegments.Length; i++)
        {
            Assert.Equal(expectedSegments[i].Text, actualSegments[i].Text);
            Assert.Equal(expectedSegments[i].Type, actualSegments[i].Type);
        }
    }

    public class NextSegmentTestData : TheoryData<string, LineSegment[]>
    {
        public NextSegmentTestData()
        {
            Add("Hello, World!",
            [
                new LineSegment("Hello", LineSegmentType.Word),
                new LineSegment(",", LineSegmentType.Punctuation),
                new LineSegment(" ", LineSegmentType.Whitespace),
                new LineSegment("World", LineSegmentType.Word),
                new LineSegment("!", LineSegmentType.Punctuation)
            ]);
            Add("   Leading and trailing spaces   ",
            [
                new LineSegment("   ", LineSegmentType.Whitespace),
                new LineSegment("Leading", LineSegmentType.Word),
                new LineSegment(" ", LineSegmentType.Whitespace),
                new LineSegment("and", LineSegmentType.Word),
                new LineSegment(" ", LineSegmentType.Whitespace),
                new LineSegment("trailing", LineSegmentType.Word),
                new LineSegment(" ", LineSegmentType.Whitespace),
                new LineSegment("spaces", LineSegmentType.Word),
                new LineSegment("   ", LineSegmentType.Whitespace)
            ]);
            Add("", []);
            Add("NoPunctuationOrSpaces",
            [
                new LineSegment("NoPunctuationOrSpaces", LineSegmentType.Word)
            ]);
            Add("A B C D E F G",
            [
                new LineSegment("A", LineSegmentType.Word),
                new LineSegment(" ", LineSegmentType.Whitespace),
                new LineSegment("B", LineSegmentType.Word),
                new LineSegment(" ", LineSegmentType.Whitespace),
                new LineSegment("C", LineSegmentType.Word),
                new LineSegment(" ", LineSegmentType.Whitespace),
                new LineSegment("D", LineSegmentType.Word),
                new LineSegment(" ", LineSegmentType.Whitespace),
                new LineSegment("E", LineSegmentType.Word),
                new LineSegment(" ", LineSegmentType.Whitespace),
                new LineSegment("F", LineSegmentType.Word),
                new LineSegment(" ", LineSegmentType.Whitespace),
                new LineSegment("G", LineSegmentType.Word),
            ]);
        }
    }
}

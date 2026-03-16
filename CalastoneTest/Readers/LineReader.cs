using System.Text;

namespace CalastoneTest.Readers;

public class LineReader(string text)
{
    private int _currentIndex = 0;
    private LineSegmentType? _previousSegmentType;

    public LineSegment? GetNextSegment()
    {
        var currentSegment = new StringBuilder();
        while (_currentIndex < text.Length)
        {
            char currentChar = text[_currentIndex];
            LineSegmentType charType = GetCharType(currentChar);
            _previousSegmentType ??= charType;

            if (_previousSegmentType != charType)
            {
                var lineSegment = new LineSegment(currentSegment.ToString(), _previousSegmentType.Value);
                _previousSegmentType = charType;
                return lineSegment;
            }

            currentSegment.Append(currentChar);
            _currentIndex++;
        }

        if (_previousSegmentType == null || currentSegment.Length == 0)
        {
            return null;
        }

        return new LineSegment(currentSegment.ToString(), _previousSegmentType.Value);
    }

    private static LineSegmentType GetCharType(char currentChar)
    {
        if (char.IsLetter(currentChar))
        {
            return LineSegmentType.Word;
        }
        else if (char.IsWhiteSpace(currentChar))
        {
            return LineSegmentType.Whitespace;
        }
        else
        {
            return LineSegmentType.Punctuation;
        }
    }
}

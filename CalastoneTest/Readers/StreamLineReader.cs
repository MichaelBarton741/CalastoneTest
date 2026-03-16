namespace CalastoneTest.Readers;

public class StreamLineReader(StreamReader reader)
{
    private LineReader? _currentLineReader;

    public LineSegment? GetNextSegment()
    {
        if (_currentLineReader != null)
        {
            var segment = _currentLineReader.GetNextSegment();
            if (segment != null)
            {
                return segment;
            }

            _currentLineReader = null;
            return new LineSegment(Environment.NewLine, LineSegmentType.NewLine);
        }

        if (reader.EndOfStream)
        {
            return null;
        }

        var line = reader.ReadLine();
        if (line == null)
        {
            return new LineSegment(Environment.NewLine, LineSegmentType.NewLine);
        }

        _currentLineReader = new LineReader(line);
        var nextSegment = _currentLineReader.GetNextSegment();
        return nextSegment;
    }
}

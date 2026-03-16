using CalastoneTest.Filters;
using CalastoneTest.Readers;
using System.Text;

namespace CalastoneTest;

public class TextFilterProcessor(IFilter[] filters)
{
    public string Process(StreamReader reader)
    {
        var lineReader = new StreamLineReader(reader);

        StringBuilder output = new();
        var currentSegment = lineReader.GetNextSegment();
        while (currentSegment != null)
        {
            var shouldExclude = currentSegment.Type == LineSegmentType.Word && filters.Any(filter => filter.IsMatch(currentSegment.Text));
            if (!shouldExclude)
            {
                output.Append(currentSegment.Text);
            }

            currentSegment = lineReader.GetNextSegment();
        }

        return output.ToString();
    }
}

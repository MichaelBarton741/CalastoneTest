using CalastoneTest.Filters;
using CalastoneTest.Readers;
using System.Text;

var filters = new IFilter[]
{
    new VowelInTheMiddleFilter(),
    new WordLessThan3Filter(),
    new ContainsTFilter(),
};

using StreamReader reader = new("Input.txt");
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

Console.WriteLine(output.ToString());
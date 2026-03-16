using CalastoneTest;
using CalastoneTest.Filters;

var filters = new IFilter[]
{
    new VowelInTheMiddleFilter(),
    new WordLessThan3Filter(),
    new ContainsTFilter(),
};

using StreamReader reader = new("Input.txt");

var processor = new TextFilterProcessor(filters);
var result = processor.Process(reader);

Console.WriteLine(result);
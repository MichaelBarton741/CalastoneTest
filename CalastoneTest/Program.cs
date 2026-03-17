using CalastoneTest;
using CalastoneTest.Filters;

var filters = new IFilter[]
{
    new VowelInTheMiddleFilter(),
    new WordLessThan3Filter(),
    new ContainsTFilter(),
};

var inputPath = args.Length > 0
    ? args[0]
    : Path.Combine(AppContext.BaseDirectory, "Input.txt");
if (!File.Exists(inputPath))
{
    Console.Error.WriteLine($"Input file not found: {inputPath}");
    return;
}

var reader = new StreamReader(inputPath);

var processor = new TextFilterProcessor(filters);
var result = await processor.Process(reader);

Console.WriteLine(result);
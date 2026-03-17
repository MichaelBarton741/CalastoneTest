using CalastoneTest.Filters;
using System.Text.RegularExpressions;

namespace CalastoneTest;

public class TextFilterProcessor(IFilter[] filters)
{
    public async Task<string> Process(StreamReader reader)
    {
        var outputs = new List<string>();

        string? currentLine = await reader.ReadLineAsync();
        while (currentLine != null)
        {
            var processedLine = Process(currentLine);
            if (!string.IsNullOrWhiteSpace(processedLine))
            {
                outputs.Add(processedLine);
            }

            currentLine = await reader.ReadLineAsync();
        }

        return string.Join(" ", outputs);
    }

    public string Process(string text)
    {
        //Regex to match words, ignoring punctuation and whitespace
        var words = Regex.Matches(text, @"\b[a-zA-Z]+\b")
                         .Select(m => m.Value)
                         .ToArray();

        var kept = words.Where(w => filters.All(f => !f.IsMatch(w))).ToList();
        return string.Join(" ", kept);
    }
}

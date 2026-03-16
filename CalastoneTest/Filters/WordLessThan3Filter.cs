namespace CalastoneTest.Filters;

public class WordLessThan3Filter : IFilter
{
    public bool IsMatch(string input)
    {
        return input.Length < 3;
    }
}

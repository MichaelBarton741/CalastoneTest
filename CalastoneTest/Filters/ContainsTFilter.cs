namespace CalastoneTest.Filters;

public class ContainsTFilter : IFilter
{
    public bool IsMatch(string input)
    {
        return input.ToLower().Contains('t');
    }
}

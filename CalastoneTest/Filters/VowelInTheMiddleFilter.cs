using System.Collections.Immutable;

namespace CalastoneTest.Filters;

public class VowelInTheMiddleFilter : IFilter
{
    private readonly static ImmutableHashSet<char> _vowels = new HashSet<char>() {'a', 'e', 'i', 'o', 'u' }.ToImmutableHashSet();

    public bool IsMatch(string input)
    {
        if (input.Length == 0)
        {
            return false;
        }

        input = input.ToLower();
        var middleIndex = (input.Length / 2);
        var middleChar = input[middleIndex];
        if (_vowels.Contains(middleChar))
        {
            return true;
        }

        var isEvenLength = input.Length % 2 == 0;
        if (isEvenLength)
        {
            var middleLeftChar = input[middleIndex - 1];
            if (_vowels.Contains(middleLeftChar))
            {
                return true;
            }
        }

        return false;
    }
}

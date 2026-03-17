# Calastone Test

This solution reads the contents of `CalastoneTest/Input.txt`, splits the text into words, applies a set of filters, and prints the remaining words back out as a space-separated string.

## How the text is split into words

`TextFilterProcessor.Process` is responsible for breaking the input text into words.

- It uses the regex `\b[a-zA-Z]+\b`
- This means only contiguous alphabetic characters are treated as words
- Punctuation, symbols, and extra whitespace are ignored
- The final output is rebuilt with `string.Join(" ", kept)` so the remaining words are separated by single spaces

Examples:

- `"Hello, world!"` becomes `Hello` and `world`
- `"Test!?   Testing"` becomes `Test` and `Testing`

## How filtering works

Each filter implements `IFilter`, which exposes a single method:

- `IsMatch(string input)`

`TextFilterProcessor` keeps a word only when **none** of the configured filters match it:

- if any filter returns `true`, the word is removed
- if all filters return `false`, the word is kept

The console app wires up these filters in `Program.cs`:

1. `VowelInTheMiddleFilter`
2. `WordLessThan3Filter`
3. `ContainsTFilter`

## Filter rules

### `VowelInTheMiddleFilter`

Removes words where the middle character is a vowel.

- For odd-length words, it checks the single middle character
- For even-length words, it checks both middle characters
- Matching is case-insensitive

Examples:

- `BAC` is removed because `A` is in the middle
- `ABEC` is removed because one of the two middle characters is `E`
- `CBA` is kept because the middle character is not a vowel

### `WordLessThan3Filter`

Removes words shorter than 3 characters.

Examples:

- `a` and `ab` are removed
- `abc` is kept

### `ContainsTFilter`

Removes words containing the letter `t`.

- Matching is case-insensitive

Examples:

- `T`, `Trust`, and `Rest` are removed
- `Hello` is kept

## Testing

The `CalastoneTest.UnitTests` project covers both the individual filters and the processor.

- Each filter has dedicated unit tests
- `TextFilterProcessorTests` verifies:
  - word extraction ignores punctuation and symbols
  - custom filters can be injected and applied
  - the full filter set produces the expected output
  - the real `Input.txt` file matches `ExpectedOutput.txt`

To run the tests:

- `dotnet test`

To run the console app:

- `dotnet run --project CalastoneTest`

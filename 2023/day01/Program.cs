using System.Diagnostics;

var numberStrings = new Dictionary<string, string>()
{
    {"one", "1"},
    {"two", "2"},
    {"three", "3"},
    {"four", "4"},
    {"five", "5"},
    {"six", "6"},
    {"seven", "7"},
    {"eight", "8"},
    {"nine", "9"},
};

var input = await File.ReadAllLinesAsync("input.txt");

var part1 = GetTotal(input);
Console.WriteLine($"Part1: {part1}");

var replaced = input.Select(x => ReplaceFirst(x) + ReplaceLast(x));
var part2 = GetTotal(replaced);
Console.WriteLine($"Part2: {part2}");

return;

int GetTotal(IEnumerable<string> inputLocal)
{
    return inputLocal
        .Select(line => line.Where(x => "0123456789".Contains(x)).ToArray())
        .Where(x => x.Length > 0)
        .Select(numbers => int.Parse($"{numbers[0]}{numbers[^1]}"))
        .Sum();
}

string ReplaceFirst(string line)
{
    for (var i = 0; i < line.Length - 1; i++)
    {
        foreach (var text in numberStrings)
        {
            if (line[i..].StartsWith(text.Key))
            {
                return $"{(i > 0 ? line[..i] : "")}{text.Value}{line[(i + text.Key.Length)..]}";
            }
        }
    }

    return line;
}

string ReplaceLast(string line)
{
    for (var i = line.Length; i >= 0; i--)
    {
        foreach (var text in numberStrings)
        {
            if (line[..i].EndsWith(text.Key))
            {
                return $"{line[..(i - text.Key.Length)]}{text.Value}{(i <= line.Length ? line[i..] : "")}";
            }
        }
    }

    return line;
}
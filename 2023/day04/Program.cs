using System.Runtime.InteropServices.Marshalling;

var lines = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .ToList();

Part1();
Part2();

return;

void Part1()
{
    var total = 0;
    foreach (var line in lines)
    {
        var matches = GetMatches(line);
        if (matches.Length > 0)
        {
            total += 1 << (matches.Length - 1);
        }
    }
    Console.WriteLine($"Part 1: {total}");
}

void Part2()
{
    int totalLineCount = lines.Count;
    MatchAndCopy(lines, ref totalLineCount);

    Console.WriteLine($"Part 2: {totalLineCount}");
}

void MatchAndCopy(List<string> matchedLines, ref int totalLineCount, int parentLevel = 0)
{
    for (var i=0; i<matchedLines.Count; i++)
    {
        var matches = GetMatches(matchedLines[i]);
        if (matches.Length > 0)
        {
            var extras = lines.Skip(parentLevel + i + 1).Take(matches.Length).ToList();
            totalLineCount += extras.Count;
            MatchAndCopy(extras, ref totalLineCount, parentLevel + i + 1);
        }
    }
}

int[] GetMatches(string line)
{
    var winningNumbers = line.Split(": ")[1]
        .Split(" | ")[0]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToList();
    var myNumbers = line.Split(": ")[1]
        .Split(" | ")[1]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToList();
    return winningNumbers.Intersect(myNumbers).ToArray();
}
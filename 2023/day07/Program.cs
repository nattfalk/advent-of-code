using System.Runtime.ExceptionServices;

var lines = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .ToList();

List<(string, int)> sortedHands = new();
List<(string, int)> sortedHandsP2 = new();
foreach (var line in lines)
{
    var hand = line.Split(' ')[0];
    var bet = int.Parse(line.Split(" ")[1]);

    var handValues = hand.Select(x => GetValue(x));
    var score = GetScore(handValues.OrderByDescending(x => x).ToList());
    var sortKey = $"{score}_{string.Join("", handValues.Select(x => x.ToString("00")))}";
    sortedHands.Add((sortKey, bet));

    handValues = hand.Select(x => GetValue(x, true));
    score = GetScore(handValues.OrderByDescending(x => x).ToList(), true);
    sortKey = $"{score}_{string.Join("", handValues.Select(x => x.ToString("00")))}";
    sortedHandsP2.Add((sortKey, bet));
}

var part1 = sortedHands
    .OrderBy(x => x.Item1)
    .Select((x, i) => x.Item2 * (i + 1))
    .Aggregate((x, y) => x + y);
Console.WriteLine($"Part 1: {part1}");

var part2 = sortedHandsP2
    .OrderBy(x => x.Item1)
    .Select((x, i) => x.Item2 * (i + 1))
    .Aggregate((x, y) => x + y);
Console.WriteLine($"Part 2: {part2}");

return;

int GetValue(char c, bool part2 = false)
{
    return c switch
    {
        'A' => 14,
        'K' => 13,
        'Q' => 12,
        'J' => part2 ? 1 : 11,
        'T' => 10,
        '9' => 9,
        '8' => 8,
        '7' => 7,
        '6' => 6,
        '5' => 5,
        '4' => 4,
        '3' => 3,
        '2' => 2,
        _ => 0
    };
}

int GetScore(List<int> values, bool part2 = false)
{
    if (part2) 
        values = values.Where(x => x != 1).ToList();

    var groupedValues = values
        .GroupBy(x => x)
        .OrderByDescending(x => x.Count())
        .ThenByDescending(x => x.Key);

    if (part2)
    {
        var max = groupedValues.FirstOrDefault()?.FirstOrDefault() ?? 14;
        var cnt = 5 - values.Count;
        Enumerable.Range(0, cnt).ToList().ForEach(x => values.Add(max));
        groupedValues = values
            .GroupBy(x => x)
            .OrderByDescending(x => x.Count())
            .ThenByDescending(x => x.Key);
    }

    if (groupedValues.Count() == 1) return 7;
    if (groupedValues.First().Count() == 4) return 6;
    if (groupedValues.Count() == 2)
    {
        var first = groupedValues.First();
        var second = groupedValues.ToArray()[1];
        if ((first.Count() == 2 && second.Count() == 3) || (first.Count() == 3 && second.Count() == 2)) return 5;
    }
    if (groupedValues.First().Count() == 3) return 4;
    if (groupedValues.Count() == 3)
    {
        var first = groupedValues.First();
        var second = groupedValues.ToArray()[1];
        if (first.Count() == 2 && second.Count() == 2) return 3;
    }

    if (groupedValues.Count() == 4 && groupedValues.First().Count() == 2) return 2;
    return 0;
}
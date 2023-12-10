using System.Diagnostics;

var sw = new Stopwatch();
sw.Start();

var lines = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .ToList();

var part1 = 0L;
var part2 = 0L;
foreach (var line in lines)
{
    var values = line.Split(' ').Select(long.Parse).ToList();
    part1 += values.Last();
    var diffList = new List<long>();
    var part2DiffList = new List<long>();
    while (true)
    {
        var diffValues = new List<long>();
        for (var i = 0; i < values.Count - 1; i++)
        {
            diffValues.Add(values[i + 1] - values[i]);
        }

        diffList.Add(diffValues.Last());
        part2DiffList.Insert(0, values.First());

        values = diffValues;

        if (diffValues.All(x => x == 0))
            break;
    }

    part1 += diffList.Sum();
    part2 += part2DiffList.Aggregate((x, y) => y - x);
}
Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

sw.Stop();
Console.WriteLine($"Total execution time: {sw.Elapsed.Minutes:00}:{sw.Elapsed.Seconds:00}.{sw.Elapsed.Milliseconds:000}");
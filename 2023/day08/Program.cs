using System.Diagnostics;

var sw = new Stopwatch();
sw.Start();

var lines = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .ToList();

var turns = lines[0].ToCharArray();

var map = new Dictionary<string, (string, string)>();
foreach (var line in lines.Skip(1))
{
    var key = line.Split(" = ")[0];
    var left = line.Split(" = ")[1].Split(", ")[0].TrimStart('(');
    var right = line.Split(" = ")[1].Split(", ")[1].TrimEnd(')');
    map.Add(key, (left, right));
}

var part1 = GetTurns("AAA", (x) => x != "ZZZ");
Console.WriteLine($"Part 1: {part1}");

var turnCounts = new List<long>();
map.Keys
    .Where(x => x.EndsWith("A"))
    .ToList()
    .ForEach(position => turnCounts.Add(GetTurns(position, (x) => !x.EndsWith('Z'))));

var j = 0L;
var max = turnCounts.Max();
while (true)
{
    j++;
    if (turnCounts.All(x => (max * j) % x == 0))
        break;
}
Console.WriteLine($"Part 2: {j * max}");

sw.Stop();
Console.WriteLine($"Total execution time: {sw.Elapsed.Minutes:00}:{sw.Elapsed.Seconds:00}.{sw.Elapsed.Microseconds:000}");

return;

long GetTurns(string location, Func<string, bool> match)
{
    var turnIndex = 0L;
    do
    {
        var turn = turns[turnIndex++ % turns.Length];
        location = turn switch
        {
            'L' => map[location].Item1,
            'R' => map[location].Item2,
            _ => location
        };
    } while (match(location));

    return turnIndex;
}
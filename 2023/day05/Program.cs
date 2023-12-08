using System.Diagnostics;

var sw = new Stopwatch();
sw.Start();

var lines = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .ToList();

var seeds = lines[0]
    .Split(": ")[1]
    .Split(' ')
    .Select(long.Parse)
    .ToList();

var i = 1;
var mapList = new List<List<S2DEntry>>();
List<S2DEntry> currentMap = new();
do
{
    var line = lines[i];
    if (line.EndsWith(" map:"))
    {
        currentMap = new();
        mapList.Add(currentMap);
        continue;
    }

    if (!string.IsNullOrWhiteSpace(line))
    {
        var values = line.Split(' ').Select(long.Parse).ToArray();
        var diff = values[0] - values[1];
        currentMap.Add(new S2DEntry(values[1], values[1] + values[2] - 1, diff));
    }
}
while (++i < lines.Count);

var minVal = long.MaxValue;
foreach (var seed in seeds)
{
    minVal = GetMinimumValue(seed, minVal);
}
Console.WriteLine($"Part 1: {minVal}");

minVal = long.MaxValue;
for (var j=0; j<seeds.Count; j+=2)
{
    var seedList = new List<(long, long)> { (seeds[j], seeds[j] + seeds[j + 1] - 1) };
    foreach (var map in mapList)
    {
        var newSeedList = new List<(long, long)>();
        foreach (var seed in seedList)
        {
            var min = seed.Item1;
            var max = seed.Item2;
            var fertilizerMatches = map
                .Where(x => Math.Max(min, x.Min) <= Math.Min(max, x.Max))
                .OrderBy(x => x.Min);
            if (fertilizerMatches.Any())
            {
                foreach (var fertilizer in fertilizerMatches)
                {
                    if (min < fertilizer.Min)
                    {
                        newSeedList.Add((min, fertilizer.Min - 1));
                        min = fertilizer.Min;
                    }

                    if (max <= fertilizer.Max)
                    {
                        newSeedList.Add((min + fertilizer.Diff, max + fertilizer.Diff));
                        break;
                    }

                    newSeedList.Add((min + fertilizer.Diff, fertilizer.Max + fertilizer.Diff));
                    min = fertilizer.Max + 1;
                }
            }
            else
            {
                newSeedList.Add((min, max));
            }
        }
        seedList = newSeedList.OrderBy(x => x.Item1).ToList();
    }

    minVal = Math.Min(minVal, seedList.Min(x => x.Item1));
}

Console.WriteLine($"Part 2: {minVal}");
sw.Stop();
Console.WriteLine($"Total execution time: {sw.Elapsed.Minutes:00}:{sw.Elapsed.Seconds:00}.{sw.Elapsed.Milliseconds:000}");

return;

long GetMinimumValue(long seed, long minVal)
{
    var localSeed = seed;
    foreach (var map in mapList)
    {
        localSeed += map.FirstOrDefault(x => x.Min <= localSeed && x.Max >= localSeed)?.Diff ?? 0;
    }
    return Math.Min(minVal, localSeed);
}

record S2DEntry(long Min, long Max, long Diff);
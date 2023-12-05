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

var newSeeds = new List<int>();
minVal = long.MaxValue;
for (var j=0; j<seeds.Count; j+=2)
{
    Console.WriteLine($"Seed group {j/2} ...");
    for (var seed=seeds[j]; seed < (seeds[j] + seeds[j + 1] - 1); seed++)
    {
        minVal = GetMinimumValue(seed, minVal);
    }
}
Console.WriteLine($"Part 2: {minVal}");

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
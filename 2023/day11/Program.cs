var lines = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .ToList();

var originalMap = lines.Select(x => x).ToList();

var tmpLines = new List<string>();
lines.ForEach(x => {
    if (x.All(y => y == '.'))
        tmpLines.Add(x);
    tmpLines.Add(x);
});
lines = tmpLines;

var w = lines[0].Length;
for (var j=w-1; j>=0; j--)
    if (lines.All(x => x[j] == '.'))
        for (var i=0; i<lines.Count; i++)
            lines[i] = lines[i].Insert(j, ".");

var galaxies = new List<(int, int)>();
for (var y=0; y<lines.Count; y++)
    for (var x=0; x<lines[0].Length; x++)
        if (lines[y][x] == '#')
            galaxies.Add((x, y));

var part1 = 0;
for (var i=0; i<galaxies.Count-1; i++)
    for (var j=i+1; j<galaxies.Count; j++)
        part1 += Math.Abs(galaxies[j].Item2 - galaxies[i].Item2) + Math.Abs(galaxies[j].Item1 - galaxies[i].Item1);

Console.WriteLine($"Part 1: {part1}");

galaxies = new List<(int, int)>();
for (var y=0; y<originalMap.Count; y++)
    for (var x=0; x<originalMap[0].Length; x++)
        if (originalMap[y][x] == '#')
            galaxies.Add((x, y));

var verticalExpansions = originalMap
    .Select((x, i) => x.All(c => c == '.') ? (i, (long)1_000_000-1) : (i, 0));
var horizontalExpansions = Enumerable.Range(0, originalMap[0].Length)
    .Select(x => originalMap.All(l => l[x] == '.') ? (x, (long)1_000_000-1) : (x, 0));

var part2 = 0L;
for (var i=0; i<galaxies.Count-1; i++)
    for (var j=i+1; j<galaxies.Count; j++)
    {
        var xMin = Math.Min(galaxies[i].Item1, galaxies[j].Item1);
        var xMax = Math.Max(galaxies[i].Item1, galaxies[j].Item1);
        var yMin = Math.Min(galaxies[i].Item2, galaxies[j].Item2);
        var yMax = Math.Max(galaxies[i].Item2, galaxies[j].Item2);

        part2 += Math.Abs(xMax - xMin) + Math.Abs(yMax - yMin);
        part2 += horizontalExpansions
            .Where(x => x.Item1 >= xMin && x.Item1 <= xMax)
            .Sum(x => x.Item2);
        part2 += verticalExpansions
            .Where(x => x.Item1 >= yMin && x.Item1 <= yMax)
            .Sum(x => x.Item2);
    }
Console.WriteLine($"Part 2: {part2}");

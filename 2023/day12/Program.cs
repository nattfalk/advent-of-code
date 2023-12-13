using System.Diagnostics;

var cache = new Dictionary<string, long>();

var lines = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .ToList();

var sw = new Stopwatch();
sw.Start();

var part1 = 0L;
var part2 = 0L;
for (var i=0; i<lines.Count; i++)
{
    var line = lines[i];
    var pattern = line.Split(' ')[0];
    var lengths = line.Split(' ')[1].Split(',').Select(int.Parse).ToArray();
    part1 += GetArrangements(pattern, lengths);

    pattern = string.Join("?", Enumerable.Repeat(pattern, 5));
    lengths = Enumerable.Repeat(lengths, 5).SelectMany(x => x.ToArray()).ToArray();
    part2 += GetArrangements(pattern, lengths);
}
Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

sw.Stop();
Console.WriteLine($"{sw.Elapsed.Minutes:00}:{sw.Elapsed.Seconds:00}:{sw.Elapsed.Milliseconds:000}");

return;

long GetArrangements(string pattern, int[] springLengths)
{
    string[] springs = springLengths
        .Select((x, i) => i < springLengths.Length - 1 ? $"{new string('#', x)}." : $"{new string('#', x)}")
        .ToArray();

    return RecursiveMatch(pattern, springs, springs.Sum(x => x.Length));
}

long RecursiveMatch(string pattern, string[] springs, int totalSpringsLength)
{
    var cacheKey = $"{pattern}_{string.Join("", springs)}";
    if (cache.TryGetValue(cacheKey, out var c))
        return c;


    if (springs.Length == 0)
        return !pattern.Contains('#') ? 1 : 0;

    var spring = springs[0];
    var availableSpace = pattern.Length - totalSpringsLength;

    var count = 0L;
    var spanPattern = pattern.AsSpan();
    for (var i=0; i<=availableSpace; i++)
    {
        if (IsValidSpringPlacement(new string('.',i) + spring, spanPattern[..(i + spring.Length)]))
            count += RecursiveMatch(pattern[(i+spring.Length)..], springs[1..], totalSpringsLength-spring.Length);
    }

    cache.Add(cacheKey, count);
    return count;
}

bool IsValidSpringPlacement(string spring, ReadOnlySpan<char> pattern)
{
    var springSpan = spring.AsSpan(); 
    for (var i=0; i<spring.Length; i++)
    {
        if ((pattern[i] == '#' || pattern[i] == '.') && pattern[i] != springSpan[i])
            return false;
    }
    return true;
}

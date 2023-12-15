using System.Diagnostics;

var lines = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .Select(x => x.ToCharArray())
    .ToArray();

var sw = new Stopwatch();
sw.Start();

TiltNorth();
var part1 = GetLoadValue();
Console.WriteLine($"Part 1: {part1}");

var cache = new Dictionary<int, int>();

var i=1;
var cacheKey = 0;
while (true)
{
    SpinCycle(i != 1);
    cacheKey = string.Join("", lines.Select(x => new string(x))).GetHashCode();
    if (cache.ContainsKey(cacheKey))
        break;
    cache.Add(cacheKey, i++);
};

var firstCycleIteration = cache[cacheKey];
var cycleCount = i - firstCycleIteration;
var cyclesLeft = (1_000_000_000 - firstCycleIteration) % cycleCount;

while (cyclesLeft-- > 0)
    SpinCycle();

var part2 = GetLoadValue();
Console.WriteLine($"Part 2: {part2}");

sw.Stop();
Console.WriteLine($"Total execution time: {sw.Elapsed.Minutes:00}:{sw.Elapsed.Seconds:00}.{sw.ElapsedMilliseconds:000}");

return;

void SpinCycle(bool tiltNorth = true)
{
    if (tiltNorth)
        TiltNorth();
    TiltWest();
    TiltSouth();
    TiltEast();
}

int GetLoadValue()
{
    var result = 0;
    for (var r=0; r<lines.Length; r++)
        for (var c=0; c<lines[0].Length; c++)
            result += lines[r][c] == 'O' ? lines.Length - r : 0;
    return result;
}

void TiltNorth()
{
    for (var c=0; c<lines[0].Length; c++)
    {
        var r = 0;
        for (var r2=r; r2<lines.Length; r2++)
        {
            if (lines[r2][c] == 'O')
            {
                lines[r2][c] = '.';
                lines[r++][c] = 'O';
            }
            else if (lines[r2][c] == '#')
                r = r2+1;
        }
    }
}

void TiltWest()
{
    for (var r=0; r<lines.Length; r++)
    {
        var c = 0;
        for (var c2=c; c2<lines[0].Length; c2++)
        {
            if (lines[r][c2] == 'O')
            {
                lines[r][c2] = '.';
                lines[r][c++] = 'O';
            }
            else if (lines[r][c2] == '#')
                c = c2+1;
        }
    }
}

void TiltSouth()
{
    for (var c=0; c<lines[0].Length; c++)
    {
        var r = lines.Length-1;
        for (var r2=r; r2>=0; r2--)
        {
            if (lines[r2][c] == 'O')
            {
                lines[r2][c] = '.';
                lines[r--][c] = 'O';
            }
            else if (lines[r2][c] == '#')
                r = r2-1;
        }
    }
}

void TiltEast()
{
    for (var r=0; r<lines.Length; r++)
    {
        var c = lines[0].Length-1;
        for (var c2=c; c2>=0; c2--)
        {
            if (lines[r][c2] == 'O')
            {
                lines[r][c2] = '.';
                lines[r][c--] = 'O';
            }
            else if (lines[r][c2] == '#')
                c = c2-1;
        }
    }
}

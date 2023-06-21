using System.Diagnostics;
using System.Text.RegularExpressions;
List<SB> _sensorsAndBeacons = new();

var _minX = int.MaxValue; 
var _maxX = int.MinValue;

parseFile();

Stopwatch sw = new();
sw.Start();
Console.WriteLine($"Day 2-1 : {Part1(2000000)}");
sw.Stop();
Console.WriteLine($" - Executiontime: {sw.ElapsedMilliseconds} ms");

sw.Reset();
sw.Start();
Console.WriteLine($"Day 2-2 : {Part2(4000000)}");
sw.Stop();
Console.WriteLine($" - Executiontime: {sw.ElapsedMilliseconds} ms");

int Part1(int rowNum)
{
    byte[] row = new byte[_maxX - _minX + 1];

    foreach(var sb in _sensorsAndBeacons)
    {
        if (Math.Abs(rowNum - sb.sy) <= sb.distance)
        {
            var l = sb.distance - Math.Abs(rowNum - sb.sy);
            for (var x = sb.sx - l; x <= sb.sx + l; x++)
                row[x-_minX] = 1;
        }
    }
    return row.Count(x => x == 1) - 1;
}

long Part2(int maxValue)
{
    var minY = _sensorsAndBeacons.Min(sb => sb.sy - sb.distance);
    var maxY = _sensorsAndBeacons.Max(sb => sb.sy + sb.distance);

    List<Pair> pairs = new();
    for (var y = 0; y<maxValue; y++)
    {
        pairs.Clear();
        foreach(var sb in _sensorsAndBeacons)
        {
            var l = sb.distance - Math.Abs(y - sb.sy);
            if (l <= 0) continue;
            pairs.Add(new Pair() { a = Math.Max(0, sb.sx - l), b = Math.Min(maxValue, sb.sx + l) });            
        }
        pairs.Sort((a, b) => a.a <= b.a ? -1 : 1);

        if (pairs.Count < 2) continue;
        var a = pairs[0].a;
        var b = pairs[0].b;
        for (var i=1; i<pairs.Count; i++)
        {
            var p2 = pairs[i];

            // Check if ranges overlap
            if (a <= p2.b && p2.a <= b)
            {
                a = a < p2.a ? a : p2.a;
                b = b > p2.b ? b : p2.b;
            }
            else
            {
                return ((long)b + 1) * 4000000 + (long)y;
            }
        }
    }
    return 0;
}

void parseFile()
{
    Regex regex = new Regex(@"^Sensor at x=(?<sx>\d+), y=(?<sy>\d+): closest beacon is at x=(?<bx>\-*\d+), y=(?<by>\-*\d+)$", RegexOptions.Compiled);

    string input = "";
    input = @"Sensor at x=2, y=18: closest beacon is at x=-2, y=15
Sensor at x=9, y=16: closest beacon is at x=10, y=16
Sensor at x=13, y=2: closest beacon is at x=15, y=3
Sensor at x=12, y=14: closest beacon is at x=10, y=16
Sensor at x=10, y=20: closest beacon is at x=10, y=16
Sensor at x=14, y=17: closest beacon is at x=10, y=16
Sensor at x=8, y=7: closest beacon is at x=2, y=10
Sensor at x=2, y=0: closest beacon is at x=2, y=10
Sensor at x=0, y=11: closest beacon is at x=2, y=10
Sensor at x=20, y=14: closest beacon is at x=25, y=17
Sensor at x=17, y=20: closest beacon is at x=21, y=22
Sensor at x=16, y=7: closest beacon is at x=15, y=3
Sensor at x=14, y=3: closest beacon is at x=15, y=3
Sensor at x=20, y=1: closest beacon is at x=15, y=3";
    input = File.ReadAllText(@"..\input.txt");
    var lines = input.Split(Environment.NewLine);

    foreach (var line in lines)
    {
        var match = regex.Match(line);
        if (!match.Success)
            continue;
        var sb = new SB
        {
            sx = int.Parse(match.Groups["sx"].Value),
            sy = int.Parse(match.Groups["sy"].Value),
            bx = int.Parse(match.Groups["bx"].Value),
            by = int.Parse(match.Groups["by"].Value)
        };
        _sensorsAndBeacons.Add(sb);

        _minX = Math.Min(_minX, sb.sx - sb.distance);
        _maxX = Math.Max(_maxX, sb.sx + sb.distance);
    }
    _sensorsAndBeacons.Sort((a, b) => a.sx <= b.sx ? -1 : 1);
}

struct Pair
{
    public int a;
    public int b;
}

struct SB
{
    public int sx;
    public int sy;
    public int bx;
    public int by;

    public int distance => Math.Abs(by - sy) + Math.Abs(bx - sx);
}
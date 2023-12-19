using System.Diagnostics;

var lines = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .ToList();

var sw = new Stopwatch();
sw.Start();

var part1 = 0L;
var vertices = new List<Vertex>();
var prevVertex = new Vertex(0, 0);
vertices.Add(prevVertex);
foreach (var line in lines)
{
    var parts = line.Split(" ");
    var dir = parts[0][0];
    var length = int.Parse(parts[1]);

    prevVertex = CalcNext(dir, length, prevVertex);
    vertices.Add(prevVertex);
    part1 += length;
}
part1 = (part1 + SolveShoelace()) / 2 + 1;
Console.WriteLine($"Part 1: {part1}");

var part2 = 0L;
vertices.Clear();
prevVertex = new Vertex(0, 0);
vertices.Add(prevVertex);
foreach (var line in lines)
{
    var parts = line.Split(" ");

    var hex = parts[2].TrimStart('(','#').TrimEnd(')');
    var dir = hex[^1] switch {
        '0' => 'R',
        '1' => 'D',
        '2' => 'L',
        '3' => 'U',
        _ => throw new ArgumentException()
    };

    var length = int.Parse(hex[..5], System.Globalization.NumberStyles.HexNumber);

    prevVertex = CalcNext(dir, length, prevVertex);
    vertices.Add(prevVertex);
    part2 += length;
}
part2 = (part2 + SolveShoelace()) / 2 + 1;
Console.WriteLine($"Part 2: {part2}");

sw.Stop();
Console.WriteLine($"Total execution time: {sw.Elapsed.Minutes:00}:{sw.Elapsed.Seconds:00}.{sw.ElapsedMilliseconds:000}");

Console.ReadKey();
return;

long SolveShoelace()
{
    long area = 0;
    foreach (var i in Enumerable.Range(0, vertices.Count))
    {
        var v1 = vertices[i];
        var v2 = vertices[(i + 1) % vertices.Count];

        area += v1.X * v2.Y - v1.Y * v2.X;
    }
    return area;
}

Vertex CalcNext(char dir, int length, Vertex previous)
{
    var x = dir switch 
    {
        'L' => previous.X - length,
        'R' => previous.X + length,
        _ => previous.X
    };
    var y = dir switch 
    {
        'U' => previous.Y - length,
        'D' => previous.Y + length,
        _ => previous.Y
    };
    
    return new Vertex(x, y);
}

record Vertex(long X, long Y);
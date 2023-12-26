using System.Diagnostics;

var map = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrEmpty(x))
    .Select(x => x.Select(y => int.Parse(y.ToString())).ToArray())
    .ToArray();

var sw = new Stopwatch();
sw.Start();

var maxX = map[0].Length;
var maxY = map.Length;

var part1 = TracePath(false);
Console.WriteLine($"Part 1: {part1}");

var part2 = TracePath(true);
Console.WriteLine($"Part 2: {part2}");

sw.Stop();
Console.WriteLine($"Total execution time: {sw.Elapsed.Minutes:00}:{sw.Elapsed.Seconds:00}.{sw.ElapsedMilliseconds:000}");

Console.ReadKey();
return;

int TracePath(bool isUltra)
{
    HashSet<int> visited = [];
    PriorityQueue<Node, int> queue = new();
    queue.Enqueue(new Node(0, 0, 1, 0, 0, 0), 0);
    queue.Enqueue(new Node(0, 0, 0, 1, 0, 0), 0);

    var minSteps = isUltra ? 4 : 0;
    var maxSteps = isUltra ? 10 : 3;

    var heatloss = 0;
    while (queue.Count > 0)
    {
        var n = queue.Dequeue();

        if (n.X == maxX - 1 && n.Y == maxY - 1 && n.Steps >= minSteps)
        {
            heatloss = n.Heatloss;
            break;
        }

        var key = n.GetHashCode();
        if (visited.Contains(key))
            continue;
        visited.Add(key);

        if (n.Steps < maxSteps)
            Visit(queue, visited, n, n.Xd != 0 ? n.Xd : 0, n.Yd != 0 ? n.Yd : 0, n.Steps);
        if (n.Steps >= minSteps)
        {
            Visit(queue, visited, n, n.Xd != 0 ? 0 : 1, n.Yd != 0 ? 0 : -1, 0);
            Visit(queue, visited, n, n.Xd != 0 ? 0 : -1, n.Yd != 0 ? 0 : 1, 0);
        }
    }
    return heatloss;
}

void Visit(PriorityQueue<Node, int> queue, HashSet<int> visited, Node n, int xd, int yd, int steps)
{
    var nx = n.X + xd;
    var ny = n.Y + yd;

    if (nx < 0 || nx >= maxX || ny < 0 || ny >= maxY)
        return;

    var newHeatloss = n.Heatloss + map[ny][nx];
    queue.Enqueue(new Node(nx, ny, xd, yd, ++steps, newHeatloss), newHeatloss);
}

record Node(int X, int Y, int Xd, int Yd, int Steps, int Heatloss)
{
    public override int GetHashCode() =>
        HashCode.Combine(X, Y, Xd, Yd, Steps);
}
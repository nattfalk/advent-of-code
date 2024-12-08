var map = File.ReadAllLines("input.txt")
    .Select(l => l.ToCharArray()).ToArray();

var dir = Dir.Up;
var (xs, ys) = GetStartPos();
List<(int, int, Dir)> path = [];

Patrol(path, xs, ys, dir);
Console.WriteLine($"Part 1: {path.DistinctBy(x => (x.Item1, x.Item2)).Count()}");

var part2 = 0;
for (var y = 0; y < map.Length; y++)
for (var x = 0; x < map[y].Length; x++)
{
    if (map[y][x] != '.')
        continue;
    map[y][x] = '#';
    var tmpPath = new List<(int, int, Dir)>();
    if (Patrol(tmpPath, xs, ys, dir))
        part2++;

    map[y][x] = '.';
}

Console.WriteLine($"Part 2: {part2}");
return;

bool Patrol(ICollection<(int, int, Dir)> path, int xs, int ys, Dir dir)
{
    while (true)
    {
        if (path.Contains((xs, ys, dir)))
            return true;

        var (x, y) = GetNextPos(xs, ys, dir);
        if (!IsInside(x, y))
        {
            path.Add((xs, ys, dir));
            break;
        }

        if (IsBlocked(x, y))
        {
            dir = GetNextDir(dir);
            continue;
        }

        path.Add((xs, ys, dir));
        xs = x;
        ys = y;
    }

    return false;
}

bool IsInside(int x, int y) => y >= 0 && y < map.Length && x >= 0 && x < map[y].Length;
bool IsBlocked(int x, int y) => map[y][x] == '#';

(int, int) GetNextPos(int x, int y, Dir dir) =>
    dir switch
    {
        Dir.Up => (x, y - 1),
        Dir.Right => (x + 1, y),
        Dir.Down => (x, y + 1),
        Dir.Left => (x - 1, y),
    };

Dir GetNextDir(Dir dir) =>
    dir switch
    {
        Dir.Up => Dir.Right,
        Dir.Right => Dir.Down,
        Dir.Down => Dir.Left,
        Dir.Left => Dir.Up,
    };

(int, int) GetStartPos()
{
    for (var y = 0; y < map.Length; y++)
    for (var x = 0; x < map[y].Length; x++)
        if (map[y][x] == '^')
            return (x, y);
    return (-1, -1);
}

internal enum Dir
{
    Up,
    Right,
    Down,
    Left
}
var map = File.ReadAllLines("input.txt")
    .Select(x => x.ToCharArray()).ToArray();
var mX = map[0].Length;
var mY = map.Length;

var part1 = 0L;
var part2 = 0L;

var totalVisited = new HashSet<(int, int)>();
while (TryGetNonProcessedPlot(out var x, out var y))
{
    var visited = new HashSet<(int, int)>();
    var fences = new List<Line>();
    var (a, p) = Walk(visited, fences, map[y][x], x, y);
    part1 += (a * p);
    part2 += (a * fences.Count);

    visited.ToList().ForEach(tuple => totalVisited.Add((tuple.Item1, tuple.Item2)));
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
return;

(int, int) Walk(ISet<(int, int)> visited, ICollection<Line> fences, char c, int x, int y)
{
    var area = 1;
    var permimeter = 0;

    visited.Add((x, y));
    AddFences(fences, c, x, y);

    Visit(x - 1, y);
    Visit(x + 1, y);
    Visit(x, y - 1);
    Visit(x, y + 1);

    return (area, permimeter);

    void Visit(int nx, int ny)
    {
        if (visited.Contains((nx, ny)))
            return;
        if (nx >= 0 && nx < mX && ny >= 0 && ny < mY && map[ny][nx] == c)
        {
            var (na, np) = Walk(visited, fences, c, nx, ny);
            area += na;
            permimeter += np;
        }
        else
            permimeter++;
    }
}

bool TryGetNonProcessedPlot(out int x, out int y)
{
    for (var i = 0; i < map.Length; i++)
    for (var j = 0; j < map[i].Length; j++)
    {
        if (totalVisited.Contains((j, i)))
            continue;
        x = j;
        y = i;
        return true;
    }

    x = -1;
    y = -1;
    return false;
}

void AddFences(ICollection<Line> fences, char c, int x, int y)
{
    if (x == 0 || map[y][x - 1] != c)
    {
        // Left fence
        var inBetween = fences.Where(f =>
            f.Side == 0 &&
            f.X1 == x && f.X2 == x &&
            (f.Y2 == y || f.Y1 == y + 1)).ToList();
        if (inBetween.Count == 2)
        {
            inBetween.ForEach(i => fences.Remove(i));
            fences.Add(new Line(x, inBetween.MinBy(i => i.Y1)!.Y1, x, inBetween.MaxBy(i => i.Y2)!.Y2, 0));
        }
        else
        {
            var existing = fences.FirstOrDefault(f =>
                f.Side == 0 &&
                f.X1 == x && f.X2 == x &&
                ((f.Y1 < y && f.Y2 == y) || (f.Y1 == y + 1 && f.Y2 > y + 1)));
            if (existing != null)
            {
                fences.Remove(existing);
                fences.Add(new Line(x, Math.Min(y, existing.Y1), x, Math.Max(y + 1, existing.Y2), 0));
            }
            else
            {
                fences.Add(new Line(x, y, x, y + 1, 0));
            }
        }
    }

    if (y == 0 || map[y - 1][x] != c)
    {
        // Top fence
        var inBetween = fences.Where(f =>
            f.Side == 1 &&
            f.Y1 == y && f.Y2 == y &&
            (f.X2 == x || f.X1 == x + 1)).ToList();
        if (inBetween.Count == 2)
        {
            inBetween.ForEach(i => fences.Remove(i));
            fences.Add(new Line(inBetween.MinBy(i => i.X1)!.X1, y, inBetween.MaxBy(i => i.X2)!.X2, y, 1));
        }
        else
        {
            var existing = fences.FirstOrDefault(f =>
                f.Side == 1 &&
                f.Y1 == y && f.Y2 == y &&
                ((f.X1 < x && f.X2 == x) || (f.X1 == x + 1 && f.X2 > x + 1)));
            if (existing != null)
            {
                fences.Remove(existing);
                fences.Add(new Line(Math.Min(x, existing.X1), y, Math.Max(x + 1, existing.X2), y, 1));
            }
            else
            {
                fences.Add(new Line(x, y, x + 1, y, 1));
            }
        }
    }

    if (x == (mX - 1) || map[y][x + 1] != c)
    {
        // Right fence
        var inBetween = fences.Where(f =>
            f.Side == 2 &&
            f.X1 == x + 1 && f.X2 == x + 1 &&
            (f.Y2 == y || f.Y1 == y + 1)).ToList();
        if (inBetween.Count == 2)
        {
            inBetween.ForEach(i => fences.Remove(i));
            fences.Add(new Line(x + 1, inBetween.MinBy(i => i.Y1)!.Y1, x + 1, inBetween.MaxBy(i => i.Y2)!.Y2, 2));
        }
        else
        {
            var existing = fences.FirstOrDefault(f =>
                f.Side == 2 &&
                f.X1 == x + 1 && f.X2 == x + 1 &&
                ((f.Y1 < y && f.Y2 == y) || (f.Y1 == y + 1 && f.Y2 > y + 1)));
            if (existing != null)
            {
                fences.Remove(existing);
                fences.Add(new Line(x + 1, Math.Min(y, existing.Y1), x + 1, Math.Max(y + 1, existing.Y2), 2));
            }
            else
            {
                fences.Add(new Line(x + 1, y, x + 1, y + 1, 2));
            }
        }
    }

    if (y == (mY - 1) || map[y + 1][x] != c)
    {
        // Bottom fence
        var inBetween = fences.Where(f =>
            f.Side == 3 &&
            f.Y1 == y + 1 && f.Y2 == y + 1 &&
            (f.X2 == x || f.X1 == x + 1)).ToList();
        if (inBetween.Count == 2)
        {
            inBetween.ForEach(i => fences.Remove(i));
            fences.Add(new Line(inBetween.MinBy(i => i.X1)!.X1, y + 1, inBetween.MaxBy(i => i.X2)!.X2, y + 1, 3));
        }
        else
        {
            var existing = fences.FirstOrDefault(f =>
                f.Side == 3 &&
                f.Y1 == y + 1 && f.Y2 == y + 1 &&
                ((f.X1 < x && f.X2 == x) || (f.X1 == x + 1 && f.X2 > x + 1)));
            if (existing != null)
            {
                fences.Remove(existing);
                fences.Add(new Line(Math.Min(x, existing.X1), y + 1, Math.Max(existing.X2, x + 1), y + 1, 3));
            }
            else
            {
                fences.Add(new Line(x, y + 1, x + 1, y + 1, 3));
            }
        }
    }
}

internal record Line(int X1, int Y1, int X2, int Y2, int Side);
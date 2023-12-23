using System.Diagnostics;

var lines = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrEmpty(x))
    .ToList();

var sw = new Stopwatch();
sw.Start();

List<Brick> bricks = [];
var i = 0;
foreach (var line in lines)
{
    var ends = line.Split('~');
    var p1 = ends[0].Split(',');
    var p2 = ends[1].Split(',');
    bricks.Add(new Brick(
        i++,
        int.Parse(p1[0]),int.Parse(p1[1]),int.Parse(p1[2]),
        int.Parse(p2[0]),int.Parse(p2[1]),int.Parse(p2[2])));
}
bricks = bricks.OrderBy(b => Math.Min(b.Z1, b.Z2)).ToList();

foreach (var brick in bricks)
{
    var minZ = Math.Min(brick.Z1, brick.Z2);
    var newZ = 1;

    var bricksUnder = bricks
        .Where(b => Math.Max(b.Z1, b.Z2) < minZ)
        .Where(b => Intersects(brick, b))
        .ToList();
    if (bricksUnder.Count > 0)
        newZ = bricksUnder.Max(b => Math.Max(b.Z1, b.Z2)) + 1;

    var dZ = minZ - newZ;
    if (dZ == 0)
        continue;
    brick.Z1 -= dZ;
    brick.Z2 -= dZ;
}

HashSet<Brick> disintegrateBricks = [];

var part1 = 0;
foreach (var brick in bricks)
{
    var z = Math.Max(brick.Z1, brick.Z2);

    var bricksAbove = bricks
        .Where(b => Math.Min(b.Z1, b.Z2) == z+1)
        .Where(b => Intersects(brick, b))
        .ToList();
    if (bricksAbove.Count == 0)
    {
        part1++;
        continue;
    }

    var safeCount = 0;
    foreach (var brickAbove in bricksAbove)
    {
        var bricksBelowCount = bricks
            .Where(b => Math.Max(b.Z1, b.Z2) == z)
            .Where(b => Intersects(brickAbove, b))
            .Count();
        if (bricksBelowCount >= 2)
            safeCount++;
        else if (bricksBelowCount == 1)
            disintegrateBricks.Add(brick);

        brick.BricksAbove.Add(brickAbove);
        brickAbove.BricksBelow.Add(brick);        
    }
    if (safeCount == bricksAbove.Count)
        part1++;

}
Console.WriteLine($"Part 1: {part1}");

var part2 = 0;
foreach(var brick in disintegrateBricks)
{
    part2 += GetAffectedCount(brick);
}
Console.WriteLine($"Part 2: {part2}");

sw.Stop();
Console.WriteLine($"Total execution time: {sw.Elapsed.Minutes:00}:{sw.Elapsed.Seconds:00}.{sw.ElapsedMilliseconds:000}");
Console.ReadLine();

return;

int GetAffectedCount(Brick brick)
{
    var directDependants = brick.BricksAbove.Where(ba => ba.BricksBelow.Count == 1).ToArray();

    int count = directDependants.Length;
    count += GetAffected(brick.Id, directDependants);

    return count;
}

int GetAffected(int baseBrickId, Brick[] dependents)
{
    var affected = bricks
        .Where(b => b.Id != baseBrickId && !dependents.Contains(b))
        .Where(b => b.BricksBelow.Count > 0)
        .Where(b => !b.BricksBelow.Except(dependents).Any()).ToList();
    var count = affected.Count;
    if (count == 0)
        return 0;
    
    affected.AddRange(dependents);
    count += GetAffected(baseBrickId, affected.ToArray());
    return count;
}

bool Intersects(Brick b1, Brick b2) =>
    Enumerable.Range(b1.X1, b1.X2-b1.X1+1).Intersect(Enumerable.Range(b2.X1, b2.X2-b2.X1+1)).Any()
    && Enumerable.Range(b1.Y1, b1.Y2-b1.Y1+1).Intersect(Enumerable.Range(b2.Y1, b2.Y2-b2.Y1+1)).Any();

class Brick(int Id, int X1, int Y1, int Z1, int X2, int Y2, int Z2)
{
    public int Id { get; } = Id;
    public int X1 { get; set; } = X1;
    public int Y1 { get; set; } = Y1;
    public int Z1 { get; set; } = Z1;
    public int X2 { get; set; } = X2;
    public int Y2 { get; set; } = Y2;
    public int Z2 { get; set; } = Z2;
    public List<Brick> BricksAbove { get; set; } = [];
    public List<Brick> BricksBelow { get; set; } = [];
}

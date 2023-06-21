HashSet<Voxel> _voxels = new();

parseFile();

var p1 = Part1();
Console.WriteLine($"Day 18-1 : {p1}");
Console.WriteLine($"Day 18-2 : {p1 - Part2()}");

void parseFile()
{
    var input = "";
    input = @"2,2,1
1,2,2
2,1,2
2,2,2
2,3,2
3,2,2
2,2,3
2,2,4
1,2,5
3,2,5
2,1,5
2,3,5
2,2,6";
    input = File.ReadAllText(@"..\input.txt");
    var lines = input.Split(Environment.NewLine);

    foreach (var line in lines)
    {
        var parts = line.Split(",");
        _voxels.Add(new Voxel
        {
            X = int.Parse(parts[0]),
            Y = int.Parse(parts[1]),
            Z = int.Parse(parts[2])
        });
    }
}

int Part1()
{
    var totalFree = 0;
    foreach (var voxel in _voxels)
    {
        var occupied = 6;
        occupied -= 
            HasAdjacent(_voxels, voxel,  1,  0,  0)
            + HasAdjacent(_voxels, voxel, -1,  0,  0)
            + HasAdjacent(_voxels, voxel,  0,  1,  0)
            + HasAdjacent(_voxels, voxel,  0, -1,  0)
            + HasAdjacent(_voxels, voxel,  0,  0,  1)
            + HasAdjacent(_voxels, voxel,  0,  0, -1);
        totalFree += occupied;
    }

    return totalFree;
}

int Part2()
{
    var minX = _voxels.Min(v => v.X)-1;
    var maxX = _voxels.Max(v => v.X)+1;
    var minY = _voxels.Min(v => v.Y)-1;
    var maxY = _voxels.Max(v => v.Y)+1;
    var minZ = _voxels.Min(v => v.Z)-1;
    var maxZ = _voxels.Max(v => v.Z)+1;
    
    Queue<Voxel> nonProcessed = new();
    HashSet<Voxel> visited = new(_voxels);
    nonProcessed.Enqueue(new Voxel { X = minX, Y = minY, Z = minZ });
    do
    {
        var v = nonProcessed.Dequeue();
        if (v.X < minX || v.X > maxX || v.Y < minY || v.Y > maxY || v.Z < minZ || v.Z > maxZ)
            continue;
        visited.Add(v);

        Enqueue(v,  1,  0,  0);
        Enqueue(v, -1,  0,  0);
        Enqueue(v,  0,  1,  0);
        Enqueue(v,  0, -1,  0);
        Enqueue(v,  0,  0,  1);
        Enqueue(v,  0,  0, -1);
    } while (nonProcessed.Any());

    void Enqueue(Voxel v, int x, int y, int z)
    {
        var nv = new Voxel { X = v.X + x, Y = v.Y + y, Z = v.Z + z };
        if (!visited.Contains(nv) && !nonProcessed.Contains(nv)) nonProcessed.Enqueue(nv);
    }

    var innerVoxels = visited.Where(v => 
        v.X >= minX + 1 && v.X < maxX
        && v.Y >= minY + 1 && v.Y < maxY
        && v.Z >= minZ + 1 && v.Z < maxZ);

    var totalFree = 0;
    foreach (var voxel in innerVoxels)
    {
        var occupied = 6;
        occupied -= 
            HasAdjacent(visited, voxel,  1,  0,  0)
            + HasAdjacent(visited, voxel, -1,  0,  0)
            + HasAdjacent(visited, voxel,  0,  1,  0)
            + HasAdjacent(visited, voxel,  0, -1,  0)
            + HasAdjacent(visited, voxel,  0,  0,  1)
            + HasAdjacent(visited, voxel,  0,  0, -1);
        totalFree += occupied;
    }
    return totalFree;
}

int HasAdjacent(HashSet<Voxel> voxels, Voxel vx, int x, int y, int z) =>
    voxels.Where(v => v.X == vx.X + x && v.Y == vx.Y + y && v.Z == vx.Z + z).Any() ? 1 : 0;

struct Voxel
{
    public int X;
    public int Y;
    public int Z;
}
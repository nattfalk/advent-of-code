var data = File.ReadAllLines(@"input.txt")
    .Select((line, y) => line.Select((c, x) => (c, x, y)).ToArray());
var mX = data.Max(x => x.Max(y => y.x)) + 1;
var mY = data.Max(x => x.Max(y => y.y)) + 1;
var map = data
    .SelectMany(x => x)
    .Where(x => x.c != '.')
    .GroupBy(x => x.c, y => new { y.x, y.y });

var antinodesPart1 = new HashSet<(int, int)>();
var antinodesPart2 = new HashSet<(int, int)>();
foreach (var group in map)
{
    if (group.Count() <= 1)
        continue;

    var antennas = group.ToArray();
    antennas.ToList().ForEach(x => antinodesPart2.Add((x.x, x.y)));

    for (var i = 0; i < antennas.Length - 1; i++)
    for (var j = i + 1; j < antennas.Length; j++)
    {
        var dx = antennas[i].x - antennas[j].x;
        var dy = antennas[i].y - antennas[j].y;

        AddAntinodes(antennas[i].x, antennas[i].y, dx, dy);
        AddAntinodes(antennas[j].x, antennas[j].y, -dx, -dy);
    }
}

Console.WriteLine($"Part 1: {antinodesPart1.Count}");
Console.WriteLine($"Part 2: {antinodesPart2.Count}");

return;

void AddAntinodes(int antennaX, int antennaY, int dx, int dy)
{
    var antinodeX = antennaX + dx;
    var antinodeY = antennaY + dy;
    if (antinodeX < 0 || antinodeY < 0 || antinodeX >= mX || antinodeY >= mY)
        return;

    antinodesPart1.Add((antinodeX, antinodeY));

    while (antinodeX >= 0 && antinodeY >= 0 && antinodeX < mX && antinodeY < mY)
    {
        antinodesPart2.Add((antinodeX, antinodeY));
        antinodeX += dx;
        antinodeY += dy;
    }
}
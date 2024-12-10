var map = File.ReadAllLines("input.txt")
    .Select(x => x.ToCharArray().Select(x => x != '.' ? int.Parse(x.ToString()) : -1).ToArray()).ToArray();

var mX = map[0].Length;
var mY = map.Length;

var trailHeads = map
    .SelectMany((r, row) => r.Select((value, col) => new { value, row, col }))
    .Where(item => item.value == 0)
    .Select(item => new { item.row, item.col });
var walkedTrails = new Dictionary<(int, int, int, int), int>();

var part1 = trailHeads.Sum(trailHead =>
    Walk(0, trailHead.col, trailHead.row, trailHead.col, trailHead.row));
Console.WriteLine($"Part 1: {part1}");

var part2 = walkedTrails.Values.Sum();
Console.WriteLine($"Part 2: {part2}");
return;

int Walk(int height, int x, int y, int initialX, int initialY)
{
    if (height == 9)
    {
        if (!walkedTrails.ContainsKey((initialX, initialY, x, y)))
        {
            walkedTrails.Add((initialX, initialY, x, y), 1);
            return 1;
        }

        walkedTrails[(initialX, initialY, x, y)]++;
        return 0;
    }

    var result = 0;
    if (x + 1 < mX && map[y][x + 1] == height + 1)
        result += Walk(height + 1, x + 1, y, initialX, initialY);
    if (x - 1 >= 0 && map[y][x - 1] == height + 1)
        result += Walk(height + 1, x - 1, y, initialX, initialY);
    if (y + 1 < mY && map[y + 1][x] == height + 1)
        result += Walk(height + 1, x, y + 1, initialX, initialY);
    if (y - 1 >= 0 && map[y - 1][x] == height + 1)
        result += Walk(height + 1, x, y - 1, initialX, initialY);

    return result;
}
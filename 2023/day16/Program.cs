using System.Diagnostics;

var map = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .ToArray();

var sw = new Stopwatch();
sw.Start();

var maxX = map[0].Length;
var maxY = map.Length;

var occupied = new byte[maxY,maxX];

var part1 = MoveLight(-1, 0, 1, 0);
Console.WriteLine($"Part 1: {part1}");

var part2 = 0;
Enumerable.Range(0, maxX).ToList().ForEach(x =>
{
    occupied = new byte[maxY,maxX];
    part2 = Math.Max(part2, MoveLight(x, -1, 0, 1));
    occupied = new byte[maxY,maxX];
    part2 = Math.Max(part2, MoveLight(x, maxY, 0, -1));
});
Enumerable.Range(0, maxY).ToList().ForEach(y => {
    occupied = new byte[maxY,maxX];
    part2 = Math.Max(part2, MoveLight(-1, y, 1, 0));
    occupied = new byte[maxY,maxX];
    part2 = Math.Max(part2, MoveLight(maxX, y, -1, 0));
});
Console.WriteLine($"Part 1: {part2}");

sw.Stop();
Console.WriteLine($"Total execution time: {sw.Elapsed.Minutes:00}:{sw.Elapsed.Seconds:00}.{sw.ElapsedMilliseconds}");
return;

int MoveLight(int x, int y, int xd, int yd)
{
    var occupiedCount = 0;
    var done = false;
    do
    {
        x += xd;
        y += yd;

        if (x < 0 || x >= maxX || y < 0 || y >= maxY)
            break;
        if ((occupied![y, x] & (xd != 0 ? 0b1 : 0b10)) > 0)
            break;

        if (occupied![y, x] == 0)
            occupiedCount++;
        
        var c = map![y][x];
        switch (c)
        {
            case '|':
                occupied![y, x] = 0b100;
                if (xd != 0)
                {
                    occupiedCount += MoveLight(x, y, 0, -1);
                    occupiedCount += MoveLight(x, y, 0, 1);
                    done = true;
                }
                else
                {
                    occupiedCount += MoveLight(x, y, 0, yd);
                }
                break;
            case '-':
                occupied![y, x] = 0b100;
                if (xd != 0)
                {
                    occupiedCount += MoveLight(x, y, xd, 0);
                }
                else
                {
                    occupiedCount += MoveLight(x, y, -1, 0);
                    occupiedCount += MoveLight(x, y, 1, 0);
                    done = true;
                }
                break;
            case '\\':
                occupied![y, x] = 0b100;
                if (xd != 0)
                    occupiedCount += MoveLight(x, y, 0, xd < 0 ? -1 : 1);
                else
                    occupiedCount += MoveLight(x, y, yd < 0 ? -1 : 1, 0);
                done = true;
                break;
            case '/':
                occupied![y, x] = 0b100;
                if (xd != 0)
                    occupiedCount += MoveLight(x, y, 0, xd < 0 ? 1 : -1);
                else
                    occupiedCount += MoveLight(x, y, yd < 0 ? 1 : -1, 0);
                done = true;
                break;
            default:
                occupied![y, x] |= (byte)(xd != 0 ? 0b1 : 0b10);
                break;
        }
    } while (!done);

    return occupiedCount;
}

var data = File.ReadAllLines("input.txt")
    .Select(x => x.ToCharArray()).ToArray();

var maxY = data.Length;
var maxX = data[0].Length;

IList<string> validWords = ["SAM", "MAS"];

var part1 = 0;
var part2 = 0;
foreach (var y in Enumerable.Range(0, maxY))
{
    foreach (var x in Enumerable.Range(0, maxX))
    {
        part1 += HorizCount(x, y)
                 + VertCount(x, y)
                 + TlbrCount(x, y)
                 + TrblCount(x, y);

        if (data[y][x] == 'A')
            part2 += IsCrossMas(x, y) ? 1 : 0;
    }
}

Console.WriteLine("Part1: " + part1);
Console.WriteLine("Part2: " + part2);
return;

char Get(int x, int y)
{
    if (y < 0 || y >= maxY || x < 0 || x >= maxX)
        return ' ';
    return data[y][x];
}

int IsXmas(int x, int y, int xs, int ys) =>
    Get(x, y) == 'X' &&
    Get(x + xs, y + ys) == 'M' &&
    Get(x + 2 * xs, y + 2 * ys) == 'A' &&
    Get(x + 3 * xs, y + 3 * ys) == 'S'
        ? 1
        : 0;

int HorizCount(int x, int y) =>
    IsXmas(x, y, 1, 0) + IsXmas(x, y, -1, 0);

int VertCount(int x, int y) =>
    IsXmas(x, y, 0, 1) + IsXmas(x, y, 0, -1);

int TlbrCount(int x, int y) =>
    IsXmas(x, y, 1, 1) + IsXmas(x, y, -1, -1);

int TrblCount(int x, int y) =>
    IsXmas(x, y, -1, 1) + IsXmas(x, y, 1, -1);

bool IsCrossMas(int x, int y) =>
    validWords.Contains($"{Get(x - 1, y - 1)}{Get(x, y)}{Get(x + 1, y + 1)}")
    && validWords.Contains($"{Get(x + 1, y - 1)}{Get(x, y)}{Get(x - 1, y + 1)}");
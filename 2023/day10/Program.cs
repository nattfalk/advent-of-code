var lines = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .ToArray();

const string UpperPipes = "|7F";
const string RightPipes = "-7J";
const string LowerPipes = "|LJ";
const string LeftPipes = "-LF";

var newMap = new List<char[]>();
Enumerable.Range(1, lines.Length).ToList()
    .ForEach(y => newMap.Add(new string(' ', lines[0].Length).ToCharArray()));

var x = 0;
var y = 0;
for (y=0; y<lines.Length; y++)
{
    x = lines[y].IndexOf('S');
    if (x >= 0) break;
}

var pathCount = TracePath(lines, x, y);
var part1 = pathCount / 2 + (pathCount % 2 == 1 ? 1 : 0);
Console.WriteLine($"Part 1: {part1}");

var uc = UpperPipes.Contains(newMap[y-1][x]);
var rc = RightPipes.Contains(newMap[y][x+1]);
var dc = LowerPipes.Contains(newMap[y+1][x]);
var lc = LeftPipes.Contains(newMap[y][x-1]);

if (uc && dc)
    newMap[y][x] = '|';
else if (lc && rc)
    newMap[y][x] = '-';
else if (uc && rc)
    newMap[y][x] = 'L';
else if (lc && uc)
    newMap[y][x] = 'J';
else if (dc && rc)
    newMap[y][x] = 'F';
else if (lc && dc)
    newMap[y][x] = '7';

var part2 = 0;
foreach (var line in newMap)
{
    var inside = false;
    var horizontalPipeStart = ' ';
    foreach (var c in line)
    {
        if (c != ' ')
        {
            if (c == '|')
                inside = !inside;
            else if (c == 'F')
                horizontalPipeStart = c;
            else if (horizontalPipeStart == 'F' && c == 'J')
                inside = !inside;
            else if (c == 'L')
                horizontalPipeStart = c;
            else if (horizontalPipeStart == 'L' && c == '7')
                inside = !inside;
        }
        else if (inside)
        {
            part2++;
        }
    }
}
Console.WriteLine($"Part 2: {part2}");

return;

int TracePath(string[] lines, int xs, int ys)
{
    var x = xs;
    var y = ys;

    List<(int, int)> visited = new() { (x,y) };

    var c = ' ';
    do
    {
        c = lines[y][x];

        if ($"S{UpperPipes}".Contains(c) && GetChar(x, y, x, y+1, LowerPipes))
            y++;
        else if ($"S{RightPipes}".Contains(c) && GetChar(x, y, x-1, y, LeftPipes))
            x--;
        else if ($"S{LowerPipes}".Contains(c) && GetChar(x, y, x, y-1, UpperPipes))
            y--;
        else if ($"S{LeftPipes}".Contains(c) && GetChar(x, y, x+1, y, RightPipes))
            x++;

        newMap[y][x] = lines[y][x];

        if (lines[y][x] =='S')
            break;

        visited.Add((x, y));
    } while (lines[y][x] != 'S');
    return visited.Count;

    bool GetChar(int px, int py, int x, int y, string valids)
    {
        if (x < 0 || x >= lines[0].Length || y < 0 || y >= lines.Length)
            return false;
        if (visited.Count >= 2 && visited[^2] == (x, y))
            return false;

        return valids.Contains(lines[y][x]) || lines[y][x] == 'S';
    }
}
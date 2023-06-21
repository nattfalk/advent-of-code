int _width, _height;
byte[,] treeMap;

parseFile();

(int part1, int part2) = Parts();

Console.WriteLine($"Part 1 : {part1}");
Console.WriteLine($"Part 2 : {part2}");

(int, int) Parts()
{
    var part1score = (_width + _height - 2) * 2;
    var part2score = int.MinValue;

    for (var y=1; y<_height-1; y++)
        for (var x=1; x<_width-1; x++)
        {
            if (IsVisible(x,y)) part1score++;
            part2score = Math.Max(part2score, ViewScore(x, y));
        }

    return (part1score, part2score);
}

bool IsVisible(int x, int y)
{
    bool top = true, bottom = true, left = true, right = true;

    for (var i=y-1; i>=0; i--)
        if (IsBlocking(x, y, x, i, ref top))
            break;
    for (var i=y+1; i<_height; i++)
        if (IsBlocking(x, y, x, i, ref bottom))
            break;
    for (var i=x-1; i>=0; i--)
        if (IsBlocking(x, y, i, y, ref left))
            break;
    for (var i=x+1; i<_width; i++)
        if (IsBlocking(x, y, i, y, ref right))
            break;
    return top || bottom || left || right;

    bool IsBlocking(int xs, int ys, int xd, int yd, ref bool block)
    {
        if (treeMap[yd,xd] >= treeMap[ys,xs])
        {
            block = false;
            return true;
        }
        return false;
    }
}

int ViewScore(int x, int y)
{
    int top = 0, bottom = 0, left = 0, right = 0;
    for (var i=y-1; i>=0; i--)
        if (IsBlocking(x, y, x, i, ref top))
            break;
    for (var i=y+1; i<_height; i++)
        if (IsBlocking(x, y, x, i, ref bottom))
            break;
    for (var i=x-1; i>=0; i--)
        if (IsBlocking(x, y, i, y, ref left))
            break;
    for (var i=x+1; i<_width; i++)
        if (IsBlocking(x, y, i, y, ref right))
            break;
    return top * bottom * left * right;

    bool IsBlocking(int xs, int ys, int xd, int yd, ref int cnt)
    {
        cnt++;
        if (treeMap[yd,xd] >= treeMap[ys,xs]) return true;
        return false;
    }
}

void parseFile()
{
    var input = File.ReadAllText(@"..\input.txt");
//     var input = @"30373
// 25512
// 65332
// 33549
// 35390";
    var lines = input.Split(Environment.NewLine);

    _width = lines[0].Length;
    _height = lines.Length;
    treeMap = new byte[_height, _width];
    for (var i=0; i<_height; i++)
        for (var j=0; j<_width; j++)
            treeMap[i,j] = byte.Parse(lines[i][j].ToString());
}

using System.Runtime.ExceptionServices;

const bool IsTest = true;

var lines  = File.ReadAllLines(IsTest ? "input_test.txt" : "input.txt")
    .ToArray();

var mapList = new List<char[]>();
var map2List = new List<char[]>();
var movementList = new List<char[]>();
var setMovement = false;
foreach (var line in lines)
{
    if (line == "")
    {
        setMovement = true;
        continue;
    }

    if (setMovement)
    {
        movementList.Add(line.ToCharArray());
    }
    else
    {
        mapList.Add(line.ToCharArray());

        var m2Row = new List<char>();
        foreach (var c in line)
        {
            switch (c)
            {
                case '#':
                    m2Row.AddRange(['#', '#']);
                    break;
                case '.':
                    m2Row.AddRange(['.', '.']);
                    break;
                case 'O':
                    m2Row.AddRange(['[', ']']);
                    break;
                case '@':
                    m2Row.AddRange(['@', '.']);
                    break;
            }
        }
        map2List.Add(m2Row.ToArray());
    }
}

var map = mapList.ToArray();
var map2 = map2List.ToArray();
var movements = movementList.SelectMany(x => x.ToArray()).ToArray();

var sx = 0;
var sy = 0;
for (var y = 0; y < map.Length; y++)
{
    for (var x = 0; x < map[y].Length; x++)
        if (map[y][x] == '@')
        {
            sx = x;
            sy = y; 
            break;
        }
    if (sx > 0 && sy > 0)
        break;
}

var sx2 = sx * 2;
var sy2 = sy;

var dx = 0;
var dy = 0;
foreach (var m in movements)
{
    switch (m)
    {
        case '^':
            dy = -1;
            dx = 0;
            break;
        case '>':
            dy = 0;
            dx = 1;
            break;
        case 'v':
            dy = 1;
            dx = 0;
            break;
        case '<':
            dy = 0;
            dx = -1;
            break;
    }

    if (TryMove(map, sx, sy, dx, dy))
    {
        sx += dx;
        sy += dy;
    }

    if (dx != 0)
    {
        if (TryMove(map2, sx2, sy2, dx, dy))
        {
            sx2 += dx;
            sy2 += dy;
        }
    }
    else
    {
        if (TryMove2(map2, sx2, sx2, sy2, dx, dy))
        {
            sx2 += dx;
            sy2 += dy;
        }
    }
}

var part1 = 0;
for (var y = 0; y < map.Length; y++)
    for (var x = 0; x < map[y].Length; x++)
    {
        if (map[y][x] == 'O')
            part1 += y * 100 + x;
    }

var part2 = 0;
for (var y = 0; y < map2.Length; y++)
    for (var x = 0; x < map2[y].Length; x++)
    {
        if (map2[y][x] == '[')
            part2 += y * 100 + x;
    }

Console.WriteLine($"Part 1 = {part1}");
Console.WriteLine($"Part 2 = {part2}");

Console.ReadKey();
return;

bool TryMove(char[][] map, int x, int y, int dx, int dy)
{
    var nx = x + dx;
    var ny = y + dy;

    var nextChar = map[ny][nx];
    if (nextChar == '.')
    {
        map[ny][nx] = map[y][x];
        map[y][x] = '.';
        return true;
    }

    switch (nextChar)
    {
        case 'O':
        case '[':
        case ']':
        {
            var status = TryMove(map, nx, ny, dx, dy);
            if (status)
            {
                map[ny][nx] = map[y][x];
                map[y][x] = '.';
            }
            return status;
        }
        default:
            return false;
    }
}

bool TryMove2(char[][] map, int x, int x2, int y, int dx, int dy)
{
    var nx = x + dx;
    var nx2 = x2 + dx;
    var ny = y + dy;

    var nextChar = map[ny][nx];
    var nextChar2 = map[ny][nx2];
    if (nextChar == '#' || nextChar2 == '#')
        return false;
    
    if (nextChar == '.' && nextChar2 == '.')
    {
        map[ny][nx] = map[y][x];
        map[y][x] = '.';
        if (nx != nx2)
        {
            map[ny][nx2] = map[y][x2];
            map[y][x2] = '.';
        }
        return true;
    }

    if (nextChar == '[' && nextChar2 == ']')
    {

    }

    if (nextChar == ']')
    {

    }
    
    if (nextChar2 == '[')
    {

    }



        switch (nextChar)
        {

            case '[':
                {
                    var status = TryMove2(map, nx, ny, dx, dy);
                    if (status)
                    {
                        map[ny][nx] = map[y][x];
                        map[y][x] = '.';
                    }
                    return status;
                }
            default:
                return false;
        }
}
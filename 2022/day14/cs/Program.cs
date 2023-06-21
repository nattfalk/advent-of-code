const bool RENDER = true;

IList<IList<Point>> lineList;

int _minX = int.MaxValue, _minY = int.MaxValue,
    _maxX = 0, _maxY = 0;
byte[,] _map;

var result = 0;
parseFile(false);
result = Part();
if (RENDER)
    Console.SetCursorPosition(0,0);
Console.WriteLine($"Day 2-1 : {result}");

parseFile(true);
result = Part() + 1;
if (RENDER)
    Console.SetCursorPosition(0,0);
Console.WriteLine($"Day 2-2 : {result}");

void parseFile(bool floor)
{
    var input = File.ReadAllText(@"..\input.txt");
//     var input = @"498,4 -> 498,6 -> 496,6
// 503,4 -> 502,4 -> 502,9 -> 494,9";
    var lines = input.Split(Environment.NewLine);

    _minX = int.MaxValue;
    _minY = int.MaxValue;
    _maxX = 0;
    _maxY = 0;
    lineList = new List<IList<Point>>();
    IList<Point> list;
    foreach (var line in lines)
    {
        list = new List<Point>();
        var points = line.Split(" -> ");
        foreach (var point in points)
        {
            var x = int.Parse(point.Split(',')[0]);
            var y = int.Parse(point.Split(',')[1]);

            _minX = Math.Min(x, _minX);
            _minY = Math.Min(y, _minY);
            _maxX = Math.Max(x, _maxX);
            _maxY = Math.Max(y, _maxY);

            list.Add(new Point { X = x, Y = y });
        }
        lineList.Add(list);
    }

    if (floor)
    {
        _minX = 500-200;
        _maxX = 500+200;
        _maxY += 2;
        list = new List<Point>();
        list.Add(new Point { X = _minX, Y = _maxY });
        list.Add(new Point { X = _maxX, Y = _maxY });
        lineList.Add(list);
    }

    _map = new byte[_maxY+1, _maxX-_minX+1];
    foreach (var line in lineList)
    {
        Drawline(line);
    }
}

void Drawline(IList<Point> line)
{
    for (var i=0; i<line.Count-1; i++)
    {
        var s = line[i];
        var e = line[i+1];

        var steps = Math.Max(Math.Abs(e.X-s.X), Math.Abs(e.Y-s.Y));
        while (steps-- >= 0)
        {
            _map[s.Y, s.X-_minX] = 1;
            s.X += Math.Sign(e.X-s.X);
            s.Y += Math.Sign(e.Y-s.Y);
        }
    }
}

int Part()
{
    if (RENDER)
        RenderMap();

    int grains = 0;
    while (true)
    {
        if (DropGrain())
            break;
        grains++;
        // Thread.Sleep(100);
    }

    return grains;
}

bool DropGrain()
{   
    var p = new Point { X = 500, Y = 0 };
    while (true)
    {
        try 
        {
            if (_map[p.Y + 1, p.X-_minX] == 0)
                p.Y++;
            else if (_map[p.Y + 1, p.X-_minX - 1] == 0)
            {
                p.Y++;
                p.X--;   
            }
            else if (_map[p.Y + 1, p.X-_minX + 1] == 0)
            {
                p.Y++;
                p.X++;   
            }
            else
            {
                if (p.X == 500 && p.Y == 0)
                    return true;
                _map[p.Y, p.X-_minX] = 2;
                if (RENDER)
                {
                    Console.SetCursorPosition(p.X-_minX, p.Y);
                    Console.Write("o");
                }
                break;
            }
        }
        catch (IndexOutOfRangeException)
        {
            return true;
        }
    }
    return false;
}

void RenderMap()
{
    Console.Clear();
    for (var y=0; y<=_maxY; y++)
    {
        for (var x=0; x<=(_maxX-_minX); x++)
        {
            var chr = _map[y,x] switch 
            {
                0 => ".",
                1 => "#",
                2 => "o",
                _ => "."
            };
            Console.Write(chr);
        }
        Console.WriteLine();
    }
}

struct Point 
{
    public int X;
    public int Y;
}
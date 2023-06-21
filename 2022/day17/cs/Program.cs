byte[] _map = new byte[2022*4+4];
string _movement;

parseFile();

Console.WriteLine($"Day 2-1 : {Part1()}");
Console.WriteLine($"Day 2-2 : {Part2()}");

void parseFile()
{
    _movement = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";
    _movement = File.ReadAllText(@"..\input.txt");

}

int Part1()
{
    var movementIndex = 0;
    var shapeCount = 0;
    var maxY = 0;
    var shape = GetNextShape(shapeCount++, 4);

    while (shapeCount <= 2022)
    {
        var free = true;
        if (_movement[movementIndex++ % _movement.Length] == '<')
        {
            if (shape.X > 0)
            {
                for (var y=0; y<shape.Height; y++)
                    free &= (_map[shape.Y + y] & (shape.Data[y] << 1)) == 0;
                if (free)
                {
                    for (var y=0; y<shape.Height; y++)
                        shape.Data[y] <<= 1;
                    shape.X--;
                }
                else
                {
                    int a = 1;
                }
            }   
        }
        else
        {
            if (shape.X < (7 - shape.Width))
            {
                for (var y=0; y<shape.Height; y++)
                    free &= (_map[shape.Y + y] & (shape.Data[y] >> 1)) == 0;
                if (free)
                {
                    for (var y=0; y<shape.Height; y++)
                        shape.Data[y] >>= 1;
                    shape.X++;
                }
                else
                {
                    int a = 1;
                }
            }
        }

        free = true;
        for (var y=0; y<shape.Height; y++)
            free &= (_map[shape.Y + y - 1] & shape.Data[y]) == 0;
        if (shape.Y > 1 && free)
        {
            shape.Y--;
        }
        else
        {
            for (int y=0; y<shape.Height; y++)
                _map[shape.Y+y] |= shape.Data[y];
            maxY = Math.Max(maxY, shape.Y+shape.Height);

            shape = GetNextShape(shapeCount++, maxY + 3);
        }

        // PrintMap(maxY);
        // Thread.Sleep(100);
    }

    return maxY - 1;
}

void PrintMap(int maxY)
{
    Console.Clear();
    var y = maxY + 5;
    for (var i=20; i>=0; i--)
    {
        var binaryFilter = 0b1000_0000;

        for (var j=0; j<7; j++)
        {
            Console.Write((_map[y] & binaryFilter) > 0 ? "#" : ".");
            binaryFilter >>= 1;
        }
        Console.WriteLine();
        y--;
        if (y < 0) break;
    }
}

Shape GetNextShape(int shapeCount, int y)
{
    return (shapeCount % 5) switch
    {
        0 => new Shape(2, y, 4, 1, new byte[1] { 0b0011_1100 }),
        1 => new Shape(2, y, 3, 3, new byte[3] { 0b0001_0000, 0b0011_1000, 0b0001_0000 }),
        2 => new Shape(2, y, 3, 3, new byte[3] { 0b0011_1000, 0b0000_1000, 0b0000_1000 }),
        3 => new Shape(2, y, 1, 4, new byte[4] { 0b0010_0000, 0b0010_0000, 0b0010_0000, 0b0010_0000 }),
        4 => new Shape(2, y, 2, 2, new byte[2] { 0b0011_0000, 0b0011_0000 }),
    };
}

int Part2()
{
    
    return 0;
}

struct Shape
{
    public Shape(int x, int y, int w, int h, byte[] data)
    {
        this.X = x;
        this.Y = y;
        this.Width = w;
        this.Height = h;
        this.Data = data;
    }
    public int X;
    public int Y;
    public int Width;
    public int Height;
    public byte[] Data;
}
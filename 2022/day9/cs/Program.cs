string[] _lines;

parseFile();

(int part1, int part2) = Parts();

Console.WriteLine($"Part 1 : {part1}");
Console.WriteLine($"Part 2 : {part2}");

(int, int) Parts()
{
    List<Pos> knots = new List<Pos>();
    Enumerable.Range(1, 10).ToList().ForEach(x => knots.Add(new Pos(0,0)));
    
    HashSet<Pos> visited = new HashSet<Pos>();
    visited.Add(new Pos(0,0));
    HashSet<Pos> visited2 = new HashSet<Pos>();
    visited2.Add(new Pos(0,0));

    foreach (var line in _lines)
    {
        var dir = line[0];
        var cnt = int.Parse(line[2..]);

        while (cnt-->0)
        {
            knots[0] = dir switch 
            {
                'U' => new Pos(knots[0].x, knots[0].y+1),
                'R' => new Pos(knots[0].x+1, knots[0].y),
                'D' => new Pos(knots[0].x, knots[0].y-1),
                'L' => new Pos(knots[0].x-1, knots[0].y),
                _ => knots[0]
            };

            for (int i=0; i<9; i++)
            {
                var s1 = knots[i];
                var s2 = knots[i+1];

                var dx = s1.x-s2.x;
                var dy = s1.y-s2.y;
                if (Math.Max(Math.Abs(dx), Math.Abs(dy)) == 2)
                {
                    knots[i+1] = new Pos(s2.x + Math.Sign(dx), s2.y + Math.Sign(dy));
                }
            }

            if (!visited.Contains(knots[1]))
                visited.Add(knots[1]);
            if (!visited2.Contains(knots[9]))
                visited2.Add(knots[9]);
        }
    }

    return (visited.Count, visited2.Count);
}

void parseFile()
{
    string input;
    input = File.ReadAllText(@"..\input.txt");
//     input = @"R 4
// U 4
// L 3
// D 1
// R 4
// D 1
// L 5
// R 2";
//     input = @"R 5
// U 8
// L 8
// D 3
// R 17
// D 10
// L 25
// U 20";
    _lines = input.Split(Environment.NewLine);
}

record struct Pos(int x, int y);
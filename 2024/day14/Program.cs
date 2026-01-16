const int width = 101; //11;
const int height = 103; //7;

var input = File.ReadAllLines("input.txt");
var robots = new List<Robot>();
foreach (var line in input)
{
    var parts = line.Split(" ");
    var p = parts[0][2..].Split(",").Select(int.Parse).ToArray();
    var v = parts[1][2..].Split(",").Select(int.Parse).ToArray();

    robots.Add(new Robot(p[0], p[1], v[0], v[1]));
}

const int seconds = 100;
var newPostions = robots
    .Select(r => new Point(
            (((r.X + (r.XDir * seconds)) % width) + width) % width,
            (((r.Y + (r.YDir * seconds)) % height) + height) % height));

var q1 = newPostions.Where(p => p.X < (width/2) && p.Y < (height/2)).Count();
var q2 = newPostions.Where(p => p.X > (width/2) && p.Y < (height/2)).Count();
var q3 = newPostions.Where(p => p.X < (width/2) && p.Y > (height/2)).Count();
var q4 = newPostions.Where(p => p.X > (width/2) && p.Y > (height/2)).Count();

Console.WriteLine($"Part 1: {q1*q2*q3*q4}");
return;

internal record Robot(int X, int Y, int XDir, int YDir);
internal record Point(int X, int Y);

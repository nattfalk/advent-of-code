string[] _lines;

parseFile();

(var part1, var part2) = Parts();

Console.WriteLine($"Day 2-1 : {part1}");
Console.WriteLine($"Day 2-2 : {part2}");

void parseFile()
{
    _lines = File.ReadAllText(@"..\input.txt")
        .Split(Environment.NewLine);

//     _lines = @"2-4,6-8
// 2-3,4-5
// 5-7,7-9
// 2-8,3-7
// 6-6,4-6
// 2-6,4-8".Split(Environment.NewLine);
}

(int, int) Parts()
{
    var total = 0;
    var total2 = 0;

    foreach(var line in _lines)
    {
        var groups = line.Split(',');

        var s1start = int.Parse(groups[0].Split('-')[0]);
        var s1end = int.Parse(groups[0].Split('-')[1]);
        var s2start = int.Parse(groups[1].Split('-')[0]);
        var s2end = int.Parse(groups[1].Split('-')[1]);

        var s1 = Enumerable.Range(s1start, s1end - s1start + 1);
        var s2 = Enumerable.Range(s2start, s2end - s2start + 1);
        var intersects = s1.Intersect(s2).Count();
        if (intersects > 0)
            total2++;

        if (intersects == s1.Count() || intersects == s2.Count())
            total++;
    }

    return (total, total2);
}

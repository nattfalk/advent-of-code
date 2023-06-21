string[] _lines;
var rucksakList = new List<Tuple<string, string>>();

parseFile();

Console.WriteLine($"Day 2-1 : {Part1()}");
Console.WriteLine($"Day 2-2 : {Part2()}");

void parseFile()
{
    _lines = File.ReadAllText(@"..\input.txt")
        .Split(Environment.NewLine);
}

int Part1()
{
    int total = 0;
    foreach(var line in _lines)
    {
        var compartment1 = line[..(line.Length/2)];
        var compartment2 = line[(line.Length/2)..];

        var overlap = compartment1.Intersect(compartment2);
        if (!overlap.Any()) continue;
        var c = overlap.First();

        if (Char.IsLower(c))
            total += c - 'a' + 1;
        else 
            total += c - 'A' + 27;
    }

    return total;
}

int Part2()
{
    var total = 0;
    for(int i=0; i<_lines.Length; i+=3)
    {
        var sack1 = _lines[i];
        var sack2 = _lines[i+1];
        var sack3 = _lines[i+2];

        var overlap = sack1.Intersect(sack2).Intersect(sack3);
        if(!overlap.Any()) continue;
        var c = overlap.First();

        if (Char.IsLower(c))
            total += c - 'a' + 1;
        else 
            total += c - 'A' + 27;
    }

    return total;
}

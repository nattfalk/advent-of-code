int _part1;
int _part2;

parseFile();

Console.WriteLine($"Day 2-1 : {_part1}");
Console.WriteLine($"Day 2-2 : {_part2}");

void parseFile()
{
    var lines = File.ReadAllText(@"..\input.txt");

    var pairs = lines
        .Split(Environment.NewLine)
        .Select(x => new Tuple<char, char>(x.Split(" ")[0][0], x.Split(" ")[1][0]))
        .ToList();
    _part1 = pairs
         .Select(x => GetScore(x))
         .Sum();
    _part2 = pairs
         .Select(x => GetScore2(x))
         .Sum();
}

int GetScore(Tuple<char, char> pair)
{
    var score = (byte)pair.Item2 - (byte)'X' + 1;
    var diff = ((byte)pair.Item2 - (byte)'X') - ((byte)pair.Item1 - (byte)'A');

    if (diff is 1 or -2) return score + 6;
    else if (diff == 0) return score + 3;
    return score + 0;
}

int GetScore2(Tuple<char, char> pair)
{
    int a = (byte)pair.Item1 - (byte)'A';

    int score = ((byte)pair.Item2 - (byte)'X') switch
    {
        0 => ((3 + a - 1) % 3) + 1,
        1 => a + 1,
        2 => ((a + 1) % 3) + 1,
        _ => 0
    };

    var diff = score - (a + 1);

    if (diff is 1 or -2) return score + 6;
    else if (diff == 0) return score + 3;
    return score + 0;
}

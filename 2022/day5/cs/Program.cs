using System.Text.RegularExpressions;

Regex regexMovement = new Regex(@"^move\ (?<qty>\d{1,2})\ from\ (?<src>\d)\ to\ (?<dest>\d)$", RegexOptions.Compiled);

string[] _lines;
List<Stack<char>> stacks = new List<Stack<char>>();
for (var i=0; i<10; i++)
    stacks.Add(new Stack<char>());
List<Tuple<int, int, int>> movements = new();

parseFile();

var part1 = ""; //Part1();
var part2 = Part2();

Console.WriteLine($"Part 1 : {part1}");
Console.WriteLine($"Part 2 : {part2}");

void parseFile()
{
    _lines = File.ReadAllText(@"..\input.txt")
        .Split(Environment.NewLine);

//      _lines = @"    [D]    
// [N] [C]    
// [Z] [M] [P]
//  1   2   3 

// move 1 from 2 to 1
// move 3 from 1 to 3
// move 2 from 2 to 1
// move 1 from 1 to 2".Split(Environment.NewLine);

    var i = 0;
    while (_lines[i].Contains('['))
    {
        var line = _lines[i];

        var j = 1;
        var k = 0;
        do
        {
            if (!Char.IsWhiteSpace(line[j]))
            {
                if (stacks[k] is null)
                    stacks[k] = new Stack<char>();
                stacks[k].Push(line[j]);
            }
            j += 4;
            k++;
        } while (j<line.Length && k<10);
        i++;
    }
    // Reverse stacks
    for (var j = 0; j<stacks.Count; j++)
    {
        if (stacks[j] is not null)
            stacks[j] = new Stack<char>(stacks[j]);
    }

    i += 2;

    while (i < _lines.Length)
    {
        var match = regexMovement.Match(_lines[i++]);
        if (match.Success)
        {
            var qty = int.Parse(match.Groups["qty"].Value);
            var src = int.Parse(match.Groups["src"].Value);
            var dest = int.Parse(match.Groups["dest"].Value);
            
            movements.Add(new Tuple<int, int, int>(qty, src, dest));
        }
    }
}

string Part1()
{
    foreach(var m in movements)
    {
        for (var ii=0; ii<m.Item1; ii++)
            stacks[m.Item3-1].Push(stacks[m.Item2-1].Pop());
    }

    var result = "";
    foreach(var stack in stacks)
    {
        if (stack.Count > 0)
            result += stack.Peek();
    }

    return result;
}

string Part2()
{
    foreach(var m in movements)
    {
        var tmp = new Stack<char>(stacks[m.Item2-1].Take(m.Item1));
        for (var ii=0; ii<m.Item1; ii++)
            stacks[m.Item2-1].Pop();

        for (var ii=0; ii<m.Item1; ii++)
            stacks[m.Item3-1].Push(tmp.Pop());
    }

    var result = "";
    foreach(var stack in stacks)
    {
        if (stack.Count > 0)
            result += stack.Peek();
    }

    return result;
}
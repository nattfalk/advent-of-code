string[] _lines;

parseFile();
Part1();

Console.WriteLine($"Part 1 : {Part1()}");
Console.WriteLine($"Part 2 : {Part2()}");

int Part1()
{
    var i = 0;
    var pairIndex = 0;
    var indexSum = 0;
    while (i < _lines.Length)
    {
        pairIndex++;
        var line1 = _lines[i++];
        var line2 = _lines[i++];
        i++;

        var l1 = new List();
        ParseLine(line1, l1);
        var l2 = new List();
        ParseLine(line2, l2);
        if (Compare(l1, l2) >= 0)
        {
            indexSum += pairIndex;
        }
    }
    return indexSum;
}

int Part2()
{
    List<string> tmp = _lines.Where(s => !string.IsNullOrEmpty(s)).ToList();
    tmp.Add("[[2]]");
    tmp.Add("[[6]]");
    tmp.Sort((a, b) => {
        if (a.Any(Char.IsDigit) && b.Any(Char.IsDigit))
        {
            var aa = string.Join("", a.Replace("[", "").Replace("]","").Split(",").Select(x => x.Length == 0 ? "00" : x.Length == 1 ? $"0{x}" : x));
            var bb = string.Join("", b.Replace("[", "").Replace("]","").Split(",").Select(x => x.Length == 0 ? "00" : x.Length == 1 ? $"0{x}" : x));
            return string.Compare(aa, bb);
        }
        else if (!a.Any(Char.IsDigit) && !b.Any(Char.IsDigit))
        {
            return a.Length - b.Length;
        }
        else if (a.Any(Char.IsDigit)) 
        {
            return 1;
        }        
        return -1;
    });

    var sum = 1;
    var packetIndex = 0;
    for (var i=0; i<tmp.Count; i++)
    {
        packetIndex++;
        if (tmp[i] == "[[2]]" || tmp[i] == "[[6]]")
            sum *= packetIndex;
    }
    
    return sum;
}

int Compare(List l1, List l2)
{
    for (var i=0; i<Math.Max(l1.Entries.Count, l2.Entries.Count); i++)
    {
        if (l2.Entries.Count == 0 || l2.Entries.Count == i) return -1;
        else if (l1.Entries.Count == i) return 1;

        var ll1 = l1.Entries[i];
        var ll2 = l2.Entries[i];
        if ((ll1.GetType() != ll2.GetType()) || ll1 is List)
        {
            if (ll1 is Number)
            {
                var t = new List();
                t.Entries.Add(ll1);
                ll1 = t;
            }
            
            if (ll2 is Number)
            {
                var t = new List();
                t.Entries.Add(ll2);
                ll2 = t;
            }

            var res = Compare((List)ll1, (List)ll2);
            if (res != 0) return res;
        }
        else if (ll1.GetType() == ll2.GetType() && ll1 is Number)
        {
            if (((Number)ll1).Value > ((Number)ll2).Value)
                return -1;
            else if (((Number)ll1).Value < ((Number)ll2).Value)
                return 1;
        }
    }
    return 0;
}

void ParseLine(string line, List list)
{
    Stack<IEntry> stack = new();
    var tmp = line[1..];
    stack.Push(list);

    var tmpStr = string.Empty;
    for (var i=0; i<tmp.Length; i++)
    {
        var chr = tmp[i];

        if (Char.IsNumber(chr))
        {
            tmpStr += chr;
        }
        else if (chr == ',' || chr == ']')
        {
            if (int.TryParse(tmpStr, out int val))
            {
                ((List)stack.Peek()).Entries.Add(new Number(val));
                tmpStr = string.Empty;
            }

            if (chr == ']')
            {
                stack.Pop();
            }
        }
        else if (chr == '[')
        {
            var tmpList = new List();
            ((List)stack.Peek()).Entries.Add(tmpList);
            stack.Push(tmpList);
        }
    }
}

void parseFile()
{
    string input;
    input = File.ReadAllText(@"..\input.txt");
//     input = @"[1,1,3,1,1]
// [1,1,5,1,1]

// [[1],[2,3,4]]
// [[1],4]

// [9]
// [[8,7,6]]

// [[4,4],4,4]
// [[4,4],4,4,4]

// [7,7,7,7]
// [7,7,7]

// []
// [3]

// [[[]]]
// [[]]

// [1,[2,[3,[4,[5,6,7]]]],8,9]
// [1,[2,[3,[4,[5,6,0]]]],8,9]";
    
    _lines = input.Split(Environment.NewLine);
}

interface IEntry {};
class Number : IEntry
{
    public Number(int val)
    {
        Value = val;
    }
    public int Value;
}

class List : IEntry
{
    public List<IEntry> Entries = new();
}

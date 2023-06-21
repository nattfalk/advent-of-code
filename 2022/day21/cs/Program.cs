using System.Diagnostics;
using System.Text.RegularExpressions;

string[] _lines;
long _lvalue;
long _rvalue;

Regex regexSingleValue = new Regex(@"^(?<value>\d+)$", RegexOptions.Compiled);
Regex regexDoubleValue = new Regex(@"(?<first_val>\d+) (?<operator>[+\-*\/]) (?<second_val>\d+)$", RegexOptions.Compiled);
Regex regexVariable = new Regex(@"[a-z]{4}", RegexOptions.Compiled);

parseFile();

Stopwatch sw = new();
sw.Start();
Console.WriteLine($"Day 21-1 : {Part1()}");
sw.Stop();
Console.WriteLine($" - Executiontime: {sw.ElapsedMilliseconds} ms");

Console.WriteLine($"Day 21-2 : {Part2()}");

void parseFile()
{
    var input = "";
    input = @"root: pppw + sjmn
dbpl: 5
cczh: sllz + lgvd
zczc: 2
ptdq: humn - dvpt
dvpt: 3
lfqf: 4
humn: 5
ljgn: 2
sjmn: drzm * dbpl
sllz: 4
pppw: cczh / lfqf
lgvd: ljgn * ptdq
drzm: hmdt - zczc
hmdt: 32";
    // input = File.ReadAllText(@"..\input.txt");
    _lines = input.Split(Environment.NewLine);

}

long Part1()
{
    Dictionary<string, string> tmp = new();
    foreach (var line in _lines)
    {
        var parts = line.Split(": ");
        tmp.Add(parts[0], parts[1]);
    }

    do
    {
        var singleNumbers = tmp.Where(x => regexSingleValue.IsMatch(x.Value)).ToList();
        foreach (var sn in singleNumbers)
        {
            var replacements = tmp.Where(x => x.Value.Contains(sn.Key)).ToList();
            foreach (var repl in replacements)
            {
                tmp[repl.Key] = repl.Value.Replace(sn.Key, sn.Value); 
            }
            tmp.Remove(sn.Key);
        }

        var doubleNumbers = tmp.Where(x => regexDoubleValue.IsMatch(x.Value)).ToList();
        foreach (var dn in doubleNumbers)
        {
            var parts = dn.Value.Split(" ");
            var v1 = long.Parse(parts[0]);
            var v2 = long.Parse(parts[2]);

            if (tmp.Count == 1)
            {
                _lvalue = v1;
                _rvalue = v2;
            }

            tmp[dn.Key] = (parts[1] switch
            {
                "+" => (v1 + v2),
                "-" => (v1 - v2),
                "*" => (v1 * v2),
                "/" => (v1 / v2),
            }).ToString();
        }
    } while (tmp.Count > 1);

    return long.Parse(tmp.First().Value);
}

int Part2()
{
    Dictionary<string, string> tmp = new();
    foreach (var line in _lines)
    {
        var parts = line.Split(": ");
        tmp.Add(parts[0], parts[1]);
    }

    var rootNode = tmp["root"];
    // tmp.Remove("root");
    var vnode = rootNode.Split(" ")[0];
    var rnode = rootNode.Split(" ")[2];

    bool isHumn;
    (isHumn, vnode) = BuildExpression(tmp, vnode);
    if (!isHumn)
    {
        (isHumn, rnode) = BuildExpression(tmp, rnode);
    }

    return 0;
}

(bool, string) BuildExpression(Dictionary<string, string> values, string node)
{
    string result = node;

    bool hasReplaced;
    bool isHumn = false;
    do
    {
        hasReplaced = false;
        var match = regexVariable.Match(result);
        if (match.Success)
        {
            if (match.Value == "humn")
            {
                isHumn = true;
                result = result.Replace(match.Value, "0");
            }
            else
                result = result.Replace(match.Value, $"({values[match.Value]})");
            hasReplaced = true;
        }
    } while (hasReplaced);

    return (isHumn, result);
}

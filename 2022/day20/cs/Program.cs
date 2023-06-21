string[] _lines;

parseFile();

Console.WriteLine($"Day 20-1 : {Part1()}");
Console.WriteLine($"Day 20-2 : {Part2()}");

void parseFile()
{
    var input = "";
    input = @"1
2
-3
3
-2
0
4";
    input = File.ReadAllText(@"..\input.txt");
    _lines = input.Split(Environment.NewLine);

}

List<Tuple<int, long>> GetList(long multiplier)
{
    List<Tuple<int, long>> values = new();
    for (var i=0; i<_lines.Length; i++)
    {
        values.Add(new Tuple<int, long>(i, long.Parse(_lines[i])*multiplier));
    }
    return values;
}

long Part1()
{
    List<Tuple<int, long>> values = GetList(1);
    MixList(values);
    return GetResult(values);
}

long Part2()
{
    List<Tuple<int, long>> values = GetList(811589153);
    for (int i=0; i<10; i++)
    {
        MixList(values);
    }
    return GetResult(values);
}

long GetResult(List<Tuple<int, long>> values)
{
    var zeroIndex = values.IndexOf(values.First(v => v.Item2 == 0));
    return values[(zeroIndex + 1000) % values.Count].Item2
        + values[(zeroIndex + 2000) % values.Count].Item2
        + values[(zeroIndex + 3000) % values.Count].Item2;
}

void MixList(List<Tuple<int, long>> values)
{
    for (var i=0; i<values.Count; i++)
    {
        var v = values.First(v => v.Item1 == i);
        var index = values.IndexOf(v);
        values.Remove(v);

        var newIndex = index + v.Item2;
        newIndex %= values.Count;
        newIndex = newIndex < 0 ? newIndex + values.Count : newIndex;

        if (newIndex == 0) newIndex = values.Count;
        values.Insert((int)newIndex, v);
    }
}

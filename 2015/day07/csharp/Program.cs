var lines = File.ReadAllLines("..\\..\\..\\..\\input.txt");

Dictionary<string, Entry> entries = new();
foreach (var line in lines)
{
    var key = line.Split(" -> ")[1];
    var tokens = line.Split(" -> ")[0].Split(" ");

    entries.Add(key, new Entry { Tokens = tokens });
}

Calculate();
var aVal = entries["a"].Value;
Console.WriteLine($"Part1 = {aVal}");

foreach (var entry in entries)
{
    entry.Value.IsProcessed = false;
}

entries["b"].SetValue(aVal);
Calculate();
Console.WriteLine($"Part2 = {entries["a"].Value}");

void Calculate()
{
    do
    {
        foreach (var kvp in entries)
        {
            var entry = kvp.Value;
            if (entry.IsProcessed)
                continue;

            var tokens = entry.Tokens;
            if (tokens.Length == 1)
            {
                var val = GetValue(tokens[0]);
                if (val == null) 
                    continue;
                entry.SetValue((ushort)val);
            }
            else if (tokens is ["NOT", _])
            {
                var val = GetValue(tokens[1]);
                if (val == null) 
                    continue;
                entry.SetValue((ushort)~val!.Value);
            }
            else
            {
                var val1 = GetValue(tokens[0]);
                var val2 = GetValue(tokens[2]);
                if (val1 == null || val2 == null) 
                    continue;
                var result = tokens[1] switch
                {
                    "AND" => (ushort)(val1.Value & val2.Value),
                    "OR" => (ushort)(val1.Value | val2.Value),
                    "LSHIFT" => (ushort)(val1.Value << val2.Value),
                    "RSHIFT" => (ushort)(val1.Value >> val2.Value),
                    _ => (ushort)0,
                };
                entry.SetValue(result);
            }
        }
    } while (entries.Values.Any(x => !x.IsProcessed));
}

ushort? GetValue(string input)
{
    if (ushort.TryParse(input, out var val))
        return val;
    if (entries[input].IsProcessed)
        return entries[input].Value;
    return null;
}

internal class Entry
{
    public string[] Tokens = default!;
    public ushort Value;
    public bool IsProcessed;

    public void SetValue(ushort value)
    {
        Value = value;
        IsProcessed = true;
    }
}

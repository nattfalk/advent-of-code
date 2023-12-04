var lines = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .ToList();

const string numbers = "0123456789";
const string numbersAndDot = $"{numbers}.";

Part1();
Part2();

return;

void Part1()
{
    var part1 = 0;
    for (var l = 0; l < lines.Count; l++)
    {
        var engineNumber = "";
        var line = lines[l];
        var isEngineNumber = false;
        for (var c = 0; c < line.Length; c++)
        {
            if (numbers.Contains(line[c]))
            {
                engineNumber += line[c];
                if (HasAdjacentSymbol(l, c))
                {
                    isEngineNumber = true;
                }
            }

            if (c != line.Length - 1 && numbers.Contains(line[c + 1])) 
                continue;
            
            if (isEngineNumber)
            {
                part1 += int.Parse(engineNumber);
                isEngineNumber = false;
            }
            engineNumber = "";
        }
    }

    Console.WriteLine($"Part1: {part1}");
    return;

    bool HasAdjacentSymbol(int l, int c)
    {
        return HasSymbolOnLine(l - 1, c - 1)
               || HasSymbolOnLine(l - 1, c)
               || HasSymbolOnLine(l - 1, c + 1)
               || HasSymbolOnLine(l, c - 1)
               || HasSymbolOnLine(l, c + 1)
               || HasSymbolOnLine(l + 1, c - 1)
               || HasSymbolOnLine(l + 1, c)
               || HasSymbolOnLine(l + 1, c + 1);
    }

    bool HasSymbolOnLine(int l, int c)
    {
        if (l < 0 || l == lines.Count || c < 0 || c == lines[l].Length) return false;
        return !numbersAndDot.Contains(lines[l][c]);
    }
}

void Part2()
{
    Dictionary<string, List<int>> gearRatios = new();
    for (var l = 0; l < lines.Count; l++)
    {
        var engineNumber = "";
        var line = lines[l];
        var isEngineNumber = false;
        var keyList = new HashSet<string>();
        for (var c = 0; c < line.Length; c++)
        {
            if (numbers.Contains(line[c]))
            {
                engineNumber += line[c];
                var key = GetAdjcentSymbolKey(l, c);
                if (key != null)
                {
                    keyList.Add(key);
                    isEngineNumber = true;
                }
            }

            if (c != line.Length - 1 && numbers.Contains(line[c + 1]))
                continue;

            if (isEngineNumber)
            {
                var engineNumberValue = int.Parse(engineNumber);
                foreach (var key in keyList)
                {
                    if (!gearRatios.ContainsKey(key))
                        gearRatios.Add(key, new());
                    gearRatios[key].Add(engineNumberValue);
                }
                keyList.Clear();
                isEngineNumber = false;
            }

            engineNumber = "";
        }
    }

    var part2 = gearRatios
        .Where(x => x.Value.Count == 2)
        .Sum(x => x.Value[0] * x.Value[1]);

    Console.WriteLine($"Part2: {part2}");
    return;

    string? GetAdjcentSymbolKey(int l, int c)
    {
        if (AdjacentSymbolExists(l - 1, c - 1)) return $"{l - 1}_{c - 1}";
        if (AdjacentSymbolExists(l - 1, c)) return $"{l - 1}_{c}";
        if (AdjacentSymbolExists(l - 1, c + 1)) return $"{l - 1}_{c + 1}";
        if (AdjacentSymbolExists(l, c - 1)) return $"{l}_{c - 1}";
        if (AdjacentSymbolExists(l, c + 1)) return $"{l}_{c + 1}";
        if (AdjacentSymbolExists(l + 1, c - 1)) return $"{l + 1}_{c - 1}";
        if (AdjacentSymbolExists(l + 1, c)) return $"{l + 1}_{c}";
        if (AdjacentSymbolExists(l + 1, c + 1)) return $"{l + 1}_{c + 1}";
        return null;
    }

    bool AdjacentSymbolExists(int l, int c)
    {
        if (l < 0 || l == lines!.Count || c < 0 || c == lines[l].Length)
            return false; 
        return lines[l][c] == '*';
    }
}
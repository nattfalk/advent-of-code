var data = File.ReadAllLines("input.txt");

var rules = new List<string>();

var processRules = true;
var part1 = 0;
var part2 = 0;
foreach (var line in data)
{
    if (string.IsNullOrEmpty(line))
    {
        processRules = false;
        continue;
    }

    if (processRules)
    {
        rules.Add(line);
    }
    else
    {
        var parts = line.Split(",");
        var valid = true;
        for (var i = 1; i < parts.Length; i++)
        {
            for (var j = 0; j < i; j++)
            {
                if (rules.Contains($"{parts[i]}|{parts[j]}"))
                {
                    valid = false;
                    break;
                }
            }
        }

        if (valid)
        {
            part1 += int.Parse(parts[parts.Length / 2]);
        }
        else
        {
            bool swapped;
            do
            {
                swapped = false;
                for (var i = 0; i < parts.Length - 1; i++)
                {
                    if (!rules.Contains($"{parts[i]}|{parts[i + 1]}"))
                    {
                        (parts[i], parts[i + 1]) = (parts[i + 1], parts[i]);
                        swapped = true;
                    }
                }
            } while (swapped);

            part2 += int.Parse(parts[parts.Length / 2]);
        }
    }
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
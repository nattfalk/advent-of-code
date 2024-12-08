var input = File.ReadAllLines("input.txt");

var data = new List<Input>();

foreach (var line in input)
{
    var parts = line.Split(": ");
    var sum = long.Parse(parts[0]);
    var numbers = parts[1].Split(" ").Select(long.Parse).ToArray();
    data.Add(new Input(sum, numbers));
}

long part1 = 0;
long part2 = 0;
foreach (var entry in data)
{
    var result = CalculateSum(entry.Numbers, entry.Sum);
    part1 += result;
    if (result == 0)
    {
        result = CalculateSum(entry.Numbers, entry.Sum, true);
        if (result == entry.Sum)
        {
            part2 += result;
        }
    }
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part1 + part2}");

return;

long CalculateSum(long[] numbers, long sum, bool useOr = false)
{
    List<Op> ops = useOr ? [Op.Add, Op.Mul, Op.Or] : [Op.Add, Op.Mul];
    var combinations = GetCombinations(ops, numbers.Length - 1);
    foreach (var comb in combinations)
    {
        var result = numbers[0];
        var i = 1;
        foreach (var op in comb)
        {
            result = op switch
            {
                Op.Add => result + numbers[i],
                Op.Mul => result * numbers[i],
                Op.Or => long.Parse($"{result}{numbers[i]}"),
                _ => result
            };
            i++;
        }

        if (result == sum)
        {
            return result;
        }
    }

    return 0;
}

IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length)
{
    if (length == 1) return list.Select(t => new T[] { t });

    return GetCombinations(list, length - 1)
        .SelectMany(t => list, (t1, t2) => t1.Concat(new T[] { t2 }));
}


enum Op
{
    Add,
    Mul,
    Or
}

record Input(long Sum, long[] Numbers);
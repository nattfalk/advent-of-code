List<Monkey> _monkies = new();

parseFile();

Console.WriteLine($"Part 1 : {Part(20, true)}");
Console.WriteLine($"Part 2 : {Part(10000, false)}");

long Part(int times, bool useFixedRelief)
{
    var relief = useFixedRelief
        ? 3
        : _monkies.Select(x => x.Divisible)
            .ToList()
            .Aggregate((long)1, (c, v) => c * v);
    for (var i=0; i<times; i++)
    {
        foreach(var monkey in _monkies)
        {
            while (monkey.Items.Any())
            {
                monkey.Inspected++;

                var item = monkey.Items.Dequeue();

                item = monkey.Operation(item);
                if (useFixedRelief)
                    item /= relief;
                else 
                    item %= relief;

                var passToMonkey = item % monkey.Divisible == 0
                    ? monkey.IfTrue
                    : monkey.IfFalse;
                _monkies[passToMonkey].Items.Enqueue(item);
            }
        }
    }

    return _monkies
        .OrderBy(x => x.Inspected)
        .TakeLast(2)
        .ToList()
        .Aggregate((long)1, (c, v) => c * v.Inspected);
}

void parseFile()
{
    string input;
    input = File.ReadAllText(@"..\input.txt");
    // input = File.ReadAllText(@"..\input_test.txt");
    var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

    foreach (var line in lines)
    {
        var l = line.Trim();
        if (l.StartsWith("Monkey "))
        {
            _monkies.Add(new Monkey());
        }
        var monkey = _monkies.Last();

        if (l.StartsWith("Starting items: "))
        {
            l["Starting items: ".Length..]
                .Split(", ")
                .ToList()
                .ForEach(x => monkey.Items.Enqueue(long.Parse(x)));
        }
        else if (l.StartsWith("Operation: "))
        {
            var a = l["Operation: new = old ".Length..].Split(' ');
            monkey.Operation = (a[0], a[1]) switch 
            {
                ("*", "old") => (x) => x * x,
                ("+", "old") => (x) => x + x,
                ("*", _) => (x) => x * long.Parse(a[1]),
                ("+", _) => (x) => x + long.Parse(a[1]),
                (_, _) => throw new NotImplementedException() 
            };
        }
        else if (l.StartsWith("Test: "))
        {
            monkey.Divisible = long.Parse(l["Test: divisible by ".Length..]);
        }
        else if (l.StartsWith("If true: "))
        {
            monkey.IfTrue = int.Parse(l["If true: throw to monkey ".Length..]);
        }
        else if (l.StartsWith("If false: "))
        {
            monkey.IfFalse = int.Parse(l["If false: throw to monkey ".Length..]);
        }
    }
}

class Monkey
{
    public Queue<long> Items { get; set; } = new();
    public Func<long, long> Operation { get; set; }
    public long Divisible { get; set; }
    public int IfTrue { get; set; }
    public int IfFalse { get; set; }
    public long Inspected { get; set; }
}

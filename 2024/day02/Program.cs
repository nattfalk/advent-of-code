
var data = File.ReadAllLines("input.txt")
    .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
    .ToList();

var part1 = 0;
foreach (var report in data)
{
    if (IsSafeInc(report) || IsSafeDesc(report))
        part1++;
}
Console.WriteLine("Part1: " + part1);

var part2 = 0;
foreach (var report in data)
{
    var safe = IsSafeInc(report)
        || Enumerable.Range(0, report.Length).Select(i => IsSafeInc(report.Where((x, i2) => i != i2).ToList())).Any(x => x)
        || IsSafeDesc(report)
        || Enumerable.Range(0, report.Length).Select(i => IsSafeDesc(report.Where((x, i2) => i != i2).ToList())).Any(x => x);
    if (safe)
        part2++;
}
Console.WriteLine("Part2: " + part2);

bool IsSafeInc(IList<int> input) => input.Skip(1).Select((x,i) => input[i] - x is > 0 and <= 3).All(x => x);
bool IsSafeDesc(IList<int> input) => input.Skip(1).Select((x,i) => x - input[i] is > 0 and <= 3).All(x => x);

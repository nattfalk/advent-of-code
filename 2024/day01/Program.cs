var lines = File.ReadAllLines("input.txt");

var data = lines.Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray());

var list1 = data.Select(x => long.Parse(x[0])).Order();
var list2 = data.Select(x => long.Parse(x[1])).Order();

Console.WriteLine("Part1: " + list1.Zip(list2, (a, b) => Math.Abs(a-b)).Sum());
Console.WriteLine("Part2: " + list1.Aggregate(0, (long sum, long x) => sum + (x * list2.Count(y => y == x))));

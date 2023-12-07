var data = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .ToList();

var times = data[0]
    .Split(':')[1]
    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();
var distances = data[1]
    .Split(':')[1]
    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

var part1 = 1L;
for (var i = 0; i < times.Length; i++)
{
    var differentWins = Enumerable.Range(1, times[i] - 1)
        .Count(x => (times[i] - x) * x > distances[i]);
    part1 *= differentWins;
}
Console.WriteLine($"Part 1: {part1}");

var time = long.Parse(string.Join("", times));
var distance = long.Parse(string.Join("", distances));
var part2 = Enumerable.Range(1, (int)time - 1)
    .Count(x => (time - (long)x) * (long)x > distance);
Console.WriteLine($"Part 2: {part2}");

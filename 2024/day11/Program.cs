var stones = File.ReadAllText("input.txt")
    .Split(" ")
    .ToDictionary(long.Parse, x => 1L);

var part1 = 0L;
var part2 = 0L;
for (var cnt = 1; cnt <= 75; cnt++)
{
    Blink();
    switch (cnt)
    {
        case 25:
            part1 = stones.Values.Sum();
            break;
        case 75:
            part2 = stones.Values.Sum();
            break;
    }
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
return;

void Blink()
{
    var newStones = new Dictionary<long, long>();
    foreach (var stone in stones)
    {
        if (stone.Key == 0)
        {
            AddStone(1, stone.Value);
        }
        else if (stone.Key.ToString().Length % 2 == 0)
        {
            var str = stone.Key.ToString();
            var val1 = long.Parse(str[..(str.Length / 2)]);
            AddStone(val1, stone.Value);

            var val2 = long.Parse(str[(str.Length / 2)..]);
            AddStone(val2, stone.Value);
        }
        else
        {
            AddStone(stone.Key * 2024, stone.Value);
        }
    }

    stones = newStones;
    return;

    void AddStone(long key, long value)
    {
        newStones.TryAdd(key, 0);
        newStones[key] += value;
    }
}
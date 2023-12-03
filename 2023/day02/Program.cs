var lines = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .ToList();

Part1();
Part2();

return;

void Part1()
{
    const int maxRed = 12;
    const int maxGreen = 13;
    const int maxBlue = 14;

    var part1 = 0;
    foreach (var line in lines)
    {
        var p1 = line.Split(": ");
        var gameId = int.Parse(p1[0].Split(' ')[1]);

        var games = p1[1].Split("; ");

        var gameOk = true;
        foreach (var game in games)
        {
            var cubes = game.Split(", ");
            foreach (var cube in cubes)
            {
                var count = int.Parse(cube.Split(' ')[0]);
                var color = cube.Split(' ')[1];

                if ((color == "red" && count > maxRed)
                    || (color == "green" && count > maxGreen)
                    || (color == "blue" && count > maxBlue))
                {
                    gameOk = false;
                    break;
                }
            }
            if (!gameOk)
                break;
        }

        if (gameOk) part1 += gameId;
    }
    Console.WriteLine($"Part1: {part1}");
}

void Part2()
{
    var part2 = 0;
    foreach (var line in lines)
    {
        var games = line.Split(": ")[1].Split("; ");

        var redMinCount = 0;
        var greenMinCount = 0;
        var blueMinCount = 0;

        foreach (var game in games)
        {
            var cubes = game.Split(", ");
            foreach (var cube in cubes)
            {
                var count = int.Parse(cube.Split(' ')[0]);
                var color = cube.Split(' ')[1];

                switch (color)
                {
                    case "red":
                        redMinCount = Math.Max(redMinCount, count);
                        break;
                    case "green":
                        greenMinCount = Math.Max(greenMinCount, count);
                        break;
                    case "blue":
                        blueMinCount = Math.Max(blueMinCount, count);
                        break;
                }
            }
        }

        part2 += (redMinCount * greenMinCount * blueMinCount);
    }
    Console.WriteLine($"Part1: {part2}");
}

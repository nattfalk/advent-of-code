using System.Text.RegularExpressions;

var input = File.ReadAllText(@"c:\projects\private\code\advent-of-code\2024\day03\input.txt");

var regex = new Regex(@"mul\((?<val1>\d{1,3}),(?<val2>\d{1,3})\)", RegexOptions.Compiled);

var part1 = 0;
foreach (Match match in regex.Matches(input))
{
    part1 += int.Parse(match.Groups["val1"].Value) * int.Parse(match.Groups["val2"].Value);
}

Console.WriteLine("Part1: " + part1);

var regex2 = new Regex(@"(do\(\)|don't\(\)|mul\((?<val1>\d{1,3}),(?<val2>\d{1,3})\))", RegexOptions.Compiled);
var isEnabled = true;
var part2 = 0;
foreach (Match match in regex2.Matches(input))
{
    switch (match.Value)
    {
        case "do()":
            isEnabled = true;
            break;
        case "don't()":
            isEnabled = false;
            break;
        default:
        {
            if (isEnabled && match.Groups["val1"].Success && match.Groups["val2"].Success)
            {
                part2 += int.Parse(match.Groups["val1"].Value) * int.Parse(match.Groups["val2"].Value);
            }

            break;
        }
    }
}

Console.WriteLine("Part2: " + part2);
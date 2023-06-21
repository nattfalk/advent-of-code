string _input;

parseFile();

var part1 = FindMarker(4);
var part2 = FindMarker(14);

Console.WriteLine($"Part 1 : {part1}");
Console.WriteLine($"Part 2 : {part2}");

void parseFile()
{
    _input = File.ReadAllText(@"..\input.txt");

    // _input = "bvwbjplbgvbhsrlpgdmjqwftvncz"; // 5
    // _input = "nppdvjthqldpwncqszvftbrmjlhg"; // 6
    // _input = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg"; // 10
    // _input = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"; // 11
}

int FindMarker(int count)
{
    var i=0;
    while (i+count < _input.Length)
    {
        if (_input.Skip(i).Take(count).Distinct().Count() == count)
            return i+count;
        i++;
    }

    return -1;
}
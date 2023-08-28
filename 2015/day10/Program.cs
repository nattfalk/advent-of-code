using System.Text;

var part1 = 0;
var sequence = "1113122113";
for (var i = 0; i < 50; i++)
{
    if (i == 40)
        part1 = sequence.Length;
    sequence = LookAndSay(sequence);
}
Console.WriteLine($"Part 1 = {part1}");
Console.WriteLine($"Part 2 = {sequence.Length}");
Console.WriteLine("Done!");

string LookAndSay(string input)
{
    var result = new StringBuilder();

    var prev = ' ';
    var cnt = 0;
    for (var i = 0; i < input.Length; i++)
    {
        if (prev != input[i])
        {
            if (i > 0)
                result.Append($"{cnt}{prev}");
            prev = input[i];
            cnt = 0;
        }
        cnt++; 
    }
    result.Append($"{cnt}{prev}");

    return result.ToString();
}

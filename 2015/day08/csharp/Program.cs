
var lines = File.ReadAllLines(@"..\..\..\..\input.txt");
var total = 0;
var unescaped = 0;
var escaped = 0;
foreach (var line in lines)
{
    total += line.Length;
    unescaped += CountUnescaped(line);
    escaped += CountEscaped(line);
}
Console.WriteLine($"Part 1 :: Total = {total}, Unescaped = {unescaped}, Diff = {total - unescaped}");
Console.WriteLine($"Part 2 :: Total = {total}, Escaped = {escaped}, Diff = {escaped - total}");

Console.WriteLine("Done!");

int CountUnescaped(string line)
{
    line = line[1..^1];
    var cnt = 0;
    for (var i = 0; i < line.Length; i++)
    {
        if (i + 1 <= line.Length && line[i] == '\\' && line[i + 1] == 'x')
        {
            i+=3;
        }
        else if (line[i] == '\\')
            i++;
        cnt++;
    }
    return cnt;
}

int CountEscaped(string line)
{
    line = line
        .Replace("\\", "\\\\")
        .Replace("\"", "\\\"");
    return $"\"{line}\"".Length;
}


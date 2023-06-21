var maxList = parseFile();

Console.WriteLine($"Day 1-1 : {maxList.Max()}");
Console.WriteLine($"Day 1-2 : {maxList.OrderDescending().Take(3).Sum()}");

IList<int> parseFile()
{
    var lines = File.ReadAllText(@"..\input1.txt");

    var groups = lines.Split(Environment.NewLine + Environment.NewLine);
    List<int> maxList = new();
    foreach(var group in groups)
    {
        maxList.Add(group.Split("\n").Select(x => int.Parse(x)).Sum());
    }
    return maxList;
}

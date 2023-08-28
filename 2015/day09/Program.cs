var distanceMap = new Dictionary<string, IDictionary<string, int>>();
var lines = File.ReadAllLines(@"..\..\..\input.txt");

foreach (var line in lines)
{
    var cities = line.Split(" = ")[0].Split(" to ");
    var distance = int.Parse(line.Split(" = ")[1]);

    if (!distanceMap.ContainsKey(cities[0]))
        distanceMap.Add(cities[0], new Dictionary<string, int>());
    distanceMap[cities[0]].Add(cities[1], distance);
    if (!distanceMap.ContainsKey(cities[1]))
        distanceMap.Add(cities[1], new Dictionary<string, int>());
    distanceMap[cities[1]].Add(cities[0], distance);
}

var cityList = distanceMap.Keys.ToArray();
var shortestDistance = int.MaxValue;
var longestDistance = 0;
var permutations = Permute(Enumerable.Range(0, cityList.Length).ToArray());
foreach (var permutation in permutations)
{
    var distance = CalculateDistance(permutation);
    shortestDistance = Math.Min(shortestDistance, distance);
    longestDistance = Math.Max(longestDistance, distance);
}

Console.WriteLine($"Part 1 = {shortestDistance}");
Console.WriteLine($"Part 2 = {longestDistance}");
Console.WriteLine("Done!");

int CalculateDistance(IList<int> indexes)
{
    var distance = 0;
    var current = cityList[indexes[0]];
    for (var i = 1; i < indexes.Count; i++)
    {
        var next = cityList[indexes[i]];
        distance += distanceMap[current][next];
        current = next;
    }
    return distance;
}

IEnumerable<IList<int>> Permute(int[] nums)
{
    var list = new List<IList<int>>();
    return DoPermute(nums, 0, nums.Length - 1, list);
}

IList<IList<int>> DoPermute(int[] nums, int start, int end, IList<IList<int>> list)
{
    if (start == end)
    {
        list.Add(new List<int>(nums));
    }
    else
    {
        for (var i = start; i <= end; i++)
        {
            Swap(ref nums[start], ref nums[i]);
            DoPermute(nums, start + 1, end, list);
            Swap(ref nums[start], ref nums[i]);
        }
    }

    return list;
    
    void Swap(ref int a, ref int b)
    {
        (a, b) = (b, a);
    }
}

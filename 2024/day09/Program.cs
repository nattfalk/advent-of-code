var diskMap = File.ReadAllText("input.txt")
    .ToCharArray()
    .Select(x => int.Parse(x.ToString()))
    .ToArray();

var filesystem = new List<int>();
var counter = 0;
var fill = true;
foreach (var value in diskMap)
{
    filesystem.AddRange(Enumerable.Repeat(fill ? counter : -1, value));
    if (fill) counter++;
    fill = !fill;
}

var filesystem2 = new int[filesystem.Count];
filesystem.CopyTo(filesystem2);

while (filesystem.Contains(-1))
{
    var lastValue = filesystem.Last();
    filesystem.RemoveAt(filesystem.Count - 1);
    if (lastValue != -1)
    {
        var firstEmptyIndex = filesystem.IndexOf(-1);
        filesystem[firstEmptyIndex] = lastValue;
    }
}

var idx = 0;
var part1 = filesystem.Sum(x => (long)x * idx++);

Console.WriteLine($"Part 1: {part1}");

var reverseFileList = filesystem2
    .Where(x => x != -1)
    .OrderDescending()
    .GroupBy(x => x)
    .Select(x => new { Value = x.Key, Count = x.Count() });

foreach (var file in reverseFileList)
{
    var freeBlockPos = -1;
    var length = 0;

    var currentFilePos = Array.IndexOf(filesystem2, file.Value);
    for (var i = 0; i < filesystem2.Length; i++)
    {
        if (filesystem2[i] == -1 && freeBlockPos == -1)
        {
            freeBlockPos = i;
            length = 0;
        }
        else if (filesystem2[i] != -1)
            freeBlockPos = -1;

        if (freeBlockPos >= 0)
            length++;

        if (freeBlockPos > -1 && length == file.Count && freeBlockPos < currentFilePos)
        {
            var idx2 = Array.IndexOf(filesystem2, file.Value);
            while (idx != -1 && idx2 < filesystem2.Length && filesystem2[idx2] == file.Value)
                filesystem2[idx2++] = -1;

            for (var j = freeBlockPos; j < freeBlockPos + file.Count; j++)
                filesystem2[j] = file.Value;
            break;
        }
    }
}

idx = 0;
var part2 = filesystem2.Sum(x => (long)(x > -1 ? x : 0) * idx++);
Console.WriteLine($"Part 2: {part2}");
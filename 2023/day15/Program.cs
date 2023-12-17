using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;

var input = File.ReadAllText("input.txt");

var sw = new Stopwatch();
sw.Start();

var commands = input.Split(",");

var part1 = commands.Select(GetHash).Sum();
Console.WriteLine($"Part 1: {part1}");

var lensMap = new Dictionary<int, List<LensConfig>>();
var regexCmds = new Regex(@"(?'lens'[a-z]+)(?'cmd'[=-]*)(?'val'\d*)", RegexOptions.Compiled);
foreach (Match match in regexCmds.Matches(input).Cast<Match>())
{
    var lens = match.Groups["lens"].Value;
    int.TryParse(match.Groups["val"].Value, out var focalLength);
    var lensIndex = GetHash(match.Groups["lens"].Value);

    switch (match.Groups["cmd"].Value)
    {
        case "=":
            if (!lensMap.ContainsKey(lensIndex))
                lensMap[lensIndex] = new List<LensConfig>();
            if (lensMap[lensIndex].Any(x => x.Lens == lens))
                lensMap[lensIndex].First(x => x.Lens == lens).FocalLength = focalLength;
            else
                lensMap[lensIndex].Add(new LensConfig(lens, focalLength));
            break;
        case "-":
            if (!lensMap.ContainsKey(lensIndex))
                continue;
            lensMap[lensIndex] = lensMap[lensIndex]!.Where(x => x.Lens != lens).ToList();
            break;
    }
}

var part2 = 0L;
foreach (var box in lensMap)
    for (var i=0; i<box.Value.Count; i++)
        part2 += (box.Key + 1) * (i + 1) * box.Value[i].FocalLength;
Console.WriteLine($"Part 2: {part2}");

sw.Stop();
Console.WriteLine($"Total execution time: {sw.Elapsed.Minutes:00}:{sw.Elapsed.Seconds:00}.{sw.ElapsedMilliseconds:000}");

int GetHash(string code) =>
    code.Select(c => (int)c).Aggregate(0, (h, c) => (c + h) * 17 % 256);

class LensConfig(string lens, int focalLength)
{
    public string Lens { get; set; } = lens;
    public int FocalLength { get; set; } = focalLength;
}
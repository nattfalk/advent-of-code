using System.Diagnostics;

var patterns = File.ReadAllText("input.txt")
    .Split(Environment.NewLine + Environment.NewLine)
    .Select(x => x.Split(Environment.NewLine).ToList())
    .ToList();

var sw = new Stopwatch();
sw.Start();

var part1 = 0;
var part2 = 0;
for (var p=0; p<patterns.Count; p++)
{
    var pattern = patterns[p];

    var columnMatch = -1;
    var rowMatch = -1;

    // Part 1
    bool match = false;
    for (var i=0; i<pattern[0].Length - 1; i++)
    {
        match = IsColumnMirror(pattern, i);
        if (match)
        {
            columnMatch = i;
            part1 += i+1;
            break;
        }
    }

    if (!match)
    {
        for (var i=0; i<pattern.Count-1; i++)
        {
            if (IsRowMirror(pattern, i))
            {
                rowMatch = i;
                part1 += 100 * (i+1);
                break;
            }
        }
    }

    // Part 2
    match = false;
    for (var i=0; i<pattern[0].Length - 1; i++)
    {
        if (i == columnMatch)
            continue;

        match = IsColumnMirrorWithMissing(pattern, i);
        if (match)
        {
            part2 += i+1;
            break;
        }
    }

    if (!match)
    {
        for (var i=0; i<pattern.Count-1; i++)
        {
            if (i == rowMatch)
                continue;

            if (IsRowMirrorWithMissing(pattern, i))
            {
                part2 += 100 * (i+1);
                break;
            }
        }
    }
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
Console.WriteLine($"Total execution time: {sw.Elapsed.Minutes:00}:{sw.Elapsed.Seconds:00}.{sw.Elapsed.Milliseconds:000}");

return;

bool IsColumnMirror(List<string> pattern, int columnIndex)
{
    if (!pattern.Select(x => x[columnIndex]).SequenceEqual(pattern.Select(x => x[columnIndex+1])))
        return false;

    var a = Math.Min(columnIndex, pattern[0].Length - 1 - (columnIndex+1));
    for (var j=0; j<a+1; j++)
    {
        if (!pattern.Select(x => x[columnIndex-j]).SequenceEqual(pattern.Select(x => x[columnIndex+j+1])))
            return false;
    }
    
    return true;
}

bool IsRowMirror(List<string> pattern, int rowIndex)
{
    if (pattern[rowIndex] != pattern[rowIndex+1])
        return false;

    var a = Math.Min(rowIndex, pattern.Count - 1 - (rowIndex+1));
    for (var j=0; j<a+1; j++)
    {
        if (pattern[rowIndex-j] != pattern[rowIndex+j+1])
            return false;
    }

    return true;
}

bool IsColumnMirrorWithMissing(List<string> pattern, int columnIndex)
{
    var fixedSmudge = false;
    (var isMatch, var offByOne) = CheckColumn(pattern, columnIndex, columnIndex+1, !fixedSmudge);
    if (!isMatch)
        return false;
    fixedSmudge |= offByOne;

    var a = Math.Min(columnIndex, pattern[0].Length - 1 - (columnIndex+1));
    for (var j=0; j<a+1; j++)
    {
        (isMatch, offByOne) = CheckColumn(pattern, columnIndex-j, columnIndex+j+1, !fixedSmudge);
        if (!isMatch)
            return false;
        fixedSmudge |= offByOne;
    }
    
    return true;
}

(bool, bool) CheckColumn(List<string> pattern, int ci1, int ci2, bool fixMissing)
{
    var c1 = pattern.Select(x => x[ci1]).ToArray();
    var c2 = pattern.Select(x => x[ci2]).ToArray();
    if (c1.SequenceEqual(c2))
        return (true, false);

    var matchCount = c1.Select((x, i) => x == c2[i] ? 1 : 0).Sum();
    if (matchCount == c1.Length - 1)
    {
        // Off by one
        return (true, true);
    }

    return (false, false);
}

bool IsRowMirrorWithMissing(List<string> pattern, int rowIndex)
{
    var fixedSmudge = false;

    (var isMatch, var offByOne) = CheckRow(pattern, rowIndex, rowIndex+1, !fixedSmudge);
    if (!isMatch)
        return false;
    fixedSmudge |= offByOne;

    var a = Math.Min(rowIndex, pattern.Count - 1 - (rowIndex+1));
    for (var j=0; j<a+1; j++)
    {
        (isMatch, offByOne) = CheckRow(pattern, rowIndex-j, rowIndex+j+1, !fixedSmudge);
        if (!isMatch)
            return false;
        fixedSmudge |= offByOne;
    }

    return true;
}

(bool, bool) CheckRow(List<string> pattern, int ri1, int ri2, bool fixMissing)
{
    var c1 = pattern[ri1];
    var c2 = pattern[ri2];
    if (c1.SequenceEqual(c2))
        return (true, false);

    var matchCount = c1.Select((x, i) => x == c2[i] ? 1 : 0).Sum();
    if (matchCount == c1.Length - 1)
    {
        // Off by one
        return (true, true);
    }

    return (false, false);
}

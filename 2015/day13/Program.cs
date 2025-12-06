using Combinatorics.Collections;

var lines = File.ReadAllLines(@"..\..\..\input.txt").Select(l => l.Split());

var data = new Dictionary<Tuple<string, string>, int>();
foreach (var line in lines)
{
    var n1 = line[0];
    var op = line[2];
    var value = int.Parse(line[3]);
    var n2 = line[line.Length - 1].TrimEnd('.');

    value = op == "gain" ? value : -value;

    data.Add(new Tuple<string, string>(n1, n2), value);
}

var max = CalulateHappiness(data);
Console.WriteLine("Part 1 = " + max);

var attendees = data.Select(x => x.Key.Item1).Distinct().ToArray();
foreach (var attendee in attendees)
{
    data.Add(new Tuple<string, string>("Me", attendee), 0);    
    data.Add(new Tuple<string, string>(attendee, "Me"), 0);    
}
max = CalulateHappiness(data);
Console.WriteLine("Part 2 = " + max);

return;

static long CalulateHappiness(Dictionary<Tuple<string, string>, int> data)
{
    var attendees = data.Select(x => x.Key.Item1).Distinct().ToArray();
    var permutations = new Permutations<string>(attendees);
    var currentMax = 0;
    foreach (var perm in permutations)
    {
        var permTotal = 0;

        for (var i=0; i<perm.Count; i++)
        {
            var n1 = perm[i];
            var n2 = perm[(i-1+perm.Count) % perm.Count];
            permTotal += data[new Tuple<string, string>(n1, n2)];

            n2 = perm[(i+1) % perm.Count];
            permTotal += data[new Tuple<string, string>(n1, n2)];
        }

        currentMax = Math.Max(currentMax, permTotal);
    }
    return currentMax;
}
using System.Text.RegularExpressions;

List<Valve> _valves = new();

parseFile();

Console.WriteLine($"Day 16-1 : {Part1()}");
Console.WriteLine($"Day 16-2 : {Part2()}");

int Part1()
{
    Valve? valve = _valves.First();
    valve.Open = true;

    var openPressure = 0;
    var totalPressure = 0;

    var minute = 1;
    while ((valve = valve?.NextClosed) is not null && minute++ <= 30)
    {
        totalPressure += openPressure;
        openPressure += valve.FlowRate;
    }

    return totalPressure;
}

int Part2()
{
    var total = 0;

    return total;
}

void parseFile()
{
    Regex regex = new Regex(@"Valve (?<code>[A-Z]{2}) has flow rate=(?<rate>\d+); tunnel[s]* lead[s]* to valve[s]* (?<nextvalves>.+)", RegexOptions.Compiled);

    var input = "";
    input = File.ReadAllText(@"..\input.txt");
    input = @"Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
Valve BB has flow rate=13; tunnels lead to valves CC, AA
Valve CC has flow rate=2; tunnels lead to valves DD, BB
Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE
Valve EE has flow rate=3; tunnels lead to valves FF, DD
Valve FF has flow rate=0; tunnels lead to valves EE, GG
Valve GG has flow rate=0; tunnels lead to valves FF, HH
Valve HH has flow rate=22; tunnel leads to valve GG
Valve II has flow rate=0; tunnels lead to valves AA, JJ
Valve JJ has flow rate=21; tunnel leads to valve II";
    var lines = input.Split(Environment.NewLine);

    foreach (var line in lines)
    {
        var match = regex.Match(line);
        if (!match.Success) continue;

        _valves.Add(new Valve {
            Code = match.Groups["code"].Value,
            FlowRate = int.Parse(match.Groups["rate"].Value),
            NextValvesValue = match.Groups["nextvalves"].Value
        });
    }
    foreach (var valve in _valves)
    {
        string[] valveCodes = valve.NextValvesValue.Split(", ");
        foreach (var valveCode in valveCodes)
        {
            valve.NextValves.Add(_valves.First(x => x.Code == valveCode));
        }
    }

}

public class Valve
{
    public string Code;
    public int FlowRate;
    public string NextValvesValue;
    public List<Valve> NextValves = new();
    public bool Open = false;
    public int TotalPressure = 0; 

    public Valve? NextClosed => NextValves.FirstOrDefault(x => !x.Open);
}

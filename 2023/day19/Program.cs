using System.Diagnostics;
using System.Text.RegularExpressions;

var regexCriteria = new Regex(@"(?'part'[a-z]+)(?'comp'[<>])(?'value'\d+):(?'next'[a-zA-Z]+)|(?'next'[a-zA-Z]+)", RegexOptions.Compiled);
var regexRating = new Regex(@"{x=(?'x'\d+),m=(?'m'\d+),a=(?'a'\d+),s=(?'s'\d+)}", RegexOptions.Compiled);

var lines = File.ReadAllLines("input.txt").ToList();

var sw = new Stopwatch();
sw.Start();

var workflows = new List<Workflow>();
var ratings = new List<Rating>();

var parseRules = true;
foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line))
    {
        parseRules = false;
        continue;
    }

    if (parseRules)
    {
        var parts = line.Split('{');
        var criterias = parts[1].TrimEnd('}').Split(',');
        var criteriasList = new List<Criteria>();
        foreach (var criteria in criterias)
        {
            foreach (Match m in regexCriteria.Matches(criteria))
            {
                _ = int.TryParse(m.Groups["value"].Value, out var value);
                criteriasList.Add(new Criteria(
                    m.Groups["part"].Value, 
                    m.Groups["comp"].Value, 
                    value,
                    m.Groups["next"].Value));
            }
        }
        workflows.Add(new Workflow(parts[0], criteriasList));
    }
    else
    {
        Match m = regexRating.Match(line);
        if (!m.Success)
            continue;

        ratings.Add(new Rating(new List<Part>() {
            new("x", int.Parse(m.Groups["x"].Value)),
            new("m", int.Parse(m.Groups["m"].Value)),
            new("a", int.Parse(m.Groups["a"].Value)),
            new("s", int.Parse(m.Groups["s"].Value))
        }));
    }
}

// Validate ratings
var part1 = 0;
foreach (var rating in ratings)
{
    if (ValidateRating(rating, "in"))
        part1 += rating.Parts.Sum(x => x.Value);
}
Console.WriteLine($"Part 1: {part1}");

var acceptSpans = new List<RatingSpan>();
CalculateAcceptanceSpans();
var part2 = acceptSpans.Sum(x => (x.XE-x.XS+1)*(x.ME-x.MS+1)*(x.AE-x.AS+1)*(x.SE-x.SS+1));
Console.WriteLine($"Part 2: {part2}");

sw.Stop();
Console.WriteLine($"Total execution time: {sw.Elapsed.Minutes:00}:{sw.Elapsed.Seconds:00}.{sw.ElapsedMilliseconds:000}");

Console.ReadKey();
return;

void CalculateAcceptanceSpans(RatingSpan? span = null, string? workflowCode = null)
{
    if (workflowCode == "R")
        return;

    var workflow = workflows.First(x => x.Code == (workflowCode ?? "in"));

    var currentRatingSpan = new RatingSpan(span);
    foreach (var criteria in workflow.Criterias)
    {
        var newSpan = new RatingSpan(currentRatingSpan, isAccept: criteria.NextCriteria == "A");
        switch (criteria.Field)
        {
            case "x":
                newSpan.XS = criteria.Comparison == "<" ? currentRatingSpan.XS : criteria.Value + 1;
                newSpan.XE = criteria.Comparison == ">" ? currentRatingSpan.XE : criteria.Value - 1;
                if (newSpan.IsAccept)
                    acceptSpans.Add(newSpan);
                else
                    CalculateAcceptanceSpans(newSpan, criteria.NextCriteria);

                currentRatingSpan.XS = criteria.Comparison == "<" ? newSpan.XE + 1 : currentRatingSpan.XS;
                currentRatingSpan.XE = criteria.Comparison == ">" ? newSpan.XS - 1 : currentRatingSpan.XE;
                break;
            case "m":
                newSpan.MS = criteria.Comparison == "<" ? currentRatingSpan.MS : criteria.Value + 1;
                newSpan.ME = criteria.Comparison == ">" ? currentRatingSpan.ME : criteria.Value - 1;
                if (newSpan.IsAccept)
                    acceptSpans.Add(newSpan);
                else
                    CalculateAcceptanceSpans(newSpan, criteria.NextCriteria);

                currentRatingSpan.MS = criteria.Comparison == "<" ? newSpan.ME + 1 : currentRatingSpan.MS;
                currentRatingSpan.ME = criteria.Comparison == ">" ? newSpan.MS - 1 : currentRatingSpan.ME;
                break;
            case "a":
                newSpan.AS = criteria.Comparison == "<" ? currentRatingSpan.AS : criteria.Value + 1;
                newSpan.AE = criteria.Comparison == ">" ? currentRatingSpan.AE : criteria.Value - 1;
                if (newSpan.IsAccept)
                    acceptSpans.Add(newSpan);
                else
                    CalculateAcceptanceSpans(newSpan, criteria.NextCriteria);

                currentRatingSpan.AS = criteria.Comparison == "<" ? newSpan.AE + 1 : currentRatingSpan.AS;
                currentRatingSpan.AE = criteria.Comparison == ">" ? newSpan.AS - 1 : currentRatingSpan.AE;
                break;
            case "s":
                newSpan.SS = criteria.Comparison == "<" ? currentRatingSpan.SS : criteria.Value + 1;
                newSpan.SE = criteria.Comparison == ">" ? currentRatingSpan.SE : criteria.Value - 1;
                if (newSpan.IsAccept)
                    acceptSpans.Add(newSpan);
                else
                    CalculateAcceptanceSpans(newSpan, criteria.NextCriteria);

                currentRatingSpan.SS = criteria.Comparison == "<" ? newSpan.SE + 1 : currentRatingSpan.SS;
                currentRatingSpan.SE = criteria.Comparison == ">" ? newSpan.SS - 1 : currentRatingSpan.SE;
                break;
            case "":
                switch (criteria.NextCriteria)
                {
                    case "A":
                        currentRatingSpan.IsAccept = criteria.NextCriteria == "A";
                        acceptSpans.Add(currentRatingSpan);
                        break;
                    case "R":
                        break;
                    default:
                        CalculateAcceptanceSpans(currentRatingSpan, criteria.NextCriteria);
                        break;
                }
                break;
        }
    }     
}

bool ValidateRating(Rating rating, string workflowCode)
{
    var workflow = workflows!.First(x => x.Code == workflowCode);

    foreach (var criteria in workflow.Criterias)
    {
        if (criteria.Field == "")
        {
            return criteria.NextCriteria switch
            {
                "A" => true,
                "R" => false,
                _ => ValidateRating(rating, criteria.NextCriteria),
            };
        }

        var part = rating.Parts.First(x => x.Code == criteria.Field);
        var isMatch = false;
        if (criteria.Comparison == "<")
            isMatch = part.Value < criteria.Value;
        else
            isMatch = part.Value > criteria.Value;
        if (isMatch)
        {
            return criteria.NextCriteria switch
            {
                "A" => true,
                "R" => false,
                _ => ValidateRating(rating, criteria.NextCriteria),
            };
        }
    }

    return false;
}

record Criteria(string Field, string Comparison, int Value, string NextCriteria);
record Workflow(string Code, List<Criteria> Criterias);

record Part(string Code, int Value);
record Rating(List<Part> Parts);

class RatingSpan(RatingSpan? defaultSpan = null, long? XS = null, long? XE = null, long? MS = null, long? ME = null, long? AS = null, long? AE = null, long? SS = null, long? SE = null, bool isAccept = false)
{
    public long XS { get; set; } = XS ?? defaultSpan?.XS ?? 1;
    public long XE { get; set; } = XE ?? defaultSpan?.XE ?? 4000;
    public long MS { get; set; } = MS ?? defaultSpan?.MS ?? 1;
    public long ME { get; set; } = ME ?? defaultSpan?.ME ?? 4000;
    public long AS { get; set; } = AS ?? defaultSpan?.AS ?? 1;
    public long AE { get; set; } = AE ?? defaultSpan?.AE ?? 4000;
    public long SS { get; set; } = SS ?? defaultSpan?.SS ?? 1;
    public long SE { get; set; } = SE ?? defaultSpan?.SE ?? 4000;
    public bool IsAccept { get; set; } = isAccept;
}

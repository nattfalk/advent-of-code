using Newtonsoft.Json.Linq;

var accountingBook = JObject.Parse(File.ReadAllText(@"..\..\..\input.json"));

var sum = Summarize(accountingBook); 
Console.WriteLine($"Part 1 = {sum}");

var sumPart2 = Summarize(accountingBook, true);
Console.WriteLine($"Part 2 = {sumPart2}");

Console.WriteLine("Done!");

static long Summarize(JToken token, bool skipRedObjects = false)
{
    var sum = 0L;
    foreach (var t in token)
    {
        if (skipRedObjects 
            && t.Type == JTokenType.Object 
            && t.Values().Any(t => t.Type == JTokenType.String && t.ToString() == "red"))
            continue;
        
        if (t.Type == JTokenType.Integer)
            sum += t.Value<int>();
        
        sum += Summarize(t, skipRedObjects);
    }
    return sum;
}
const int WIDTH = 40;

string[] _lines;
char[] _crt = new char[WIDTH*6];

parseFile();

Console.WriteLine($"Part 1 : {Part1()}");

Console.WriteLine($"Part 2 :");
foreach(var chunk in _crt.Chunk(WIDTH))
{
    Console.WriteLine(new string(chunk));
}

int Part1()
{
    var cycleCount = 0;
    var x = 1;
    var freqVal = 0;
    foreach(var line in _lines)
    {
        var parts = line.Split(" ");
        var instr = parts[0];
        var val = parts.Length > 1 ? int.Parse(parts[1]) : 0;

        switch (instr)
        {
            case "addx":
                Draw();
                cycleCount++;
                freqVal += FreqCalc();

                Draw();
                cycleCount++;
                freqVal += FreqCalc();
                x += val;
                break;

            case "noop":
                Draw();
                cycleCount++;
                freqVal += FreqCalc();
                break;
        }
    }

    return freqVal;

    int FreqCalc() => ((cycleCount + 20) % WIDTH == 0) ? cycleCount * x : 0; 

    void Draw()
    {
        Math.DivRem(cycleCount, WIDTH, out int beamPos);
        _crt[cycleCount] = (beamPos >= (x - 1) && beamPos < (x+2)) ? '#' : '.';
    }
}

void parseFile()
{
    string input;
    input = File.ReadAllText(@"..\input.txt");
    // input = File.ReadAllText(@"..\input_test.txt");
    _lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
}

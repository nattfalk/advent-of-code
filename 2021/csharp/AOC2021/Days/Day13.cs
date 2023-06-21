using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021.Days
{
    public class Day13
    {
        private string ReadFile()
        {
            return File.ReadAllText(@"..\..\..\days\inputs\day13.txt");
        }

        public string[] ParseInput(string input)
        {
            string[] lines = input
                .Split(Environment.NewLine)
                .ToArray();
            return lines;
        }

        private List<string> Fold(string[] lines, bool breakAfterFirstFold = false)
        {
            bool parseDots = true;
            List<string> dotList = new();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    parseDots = false;
                    continue;
                }

                if (parseDots)
                {
                    dotList.Add(line);
                }
                else
                {
                    var parts = line.Split('=');
                    var foldLine = int.Parse(parts[1]);
                    if (parts[0] == "fold along y")
                    {
                        List<string> foldedDots = new();
                        foreach (var dot in dotList)
                        {
                            var newDot = dot;
                            var dotCoords = dot.Split(',');
                            var y = int.Parse(dotCoords[1]);
                            if (y > foldLine)
                            {
                                newDot = $"{dotCoords[0]},{(2 * foldLine) - y}";
                            }
                            if (!foldedDots.Contains(newDot)) foldedDots.Add(newDot);
                        }
                        dotList = foldedDots;
                        if (breakAfterFirstFold)
                            break;
                    }
                    else
                    {
                        List<string> foldedDots = new();
                        foreach (var dot in dotList)
                        {
                            var newDot = dot;
                            var dotCoords = dot.Split(',');
                            var x = int.Parse(dotCoords[0]);
                            if (x > foldLine)
                            {
                                newDot = $"{(2 * foldLine) - x},{dotCoords[1]}";
                            }
                            if (!foldedDots.Contains(newDot)) foldedDots.Add(newDot);
                        }
                        dotList = foldedDots;
                        if (breakAfterFirstFold)
                            break;
                    }
                }
            }

            return dotList;
        }

        public int ProcessPart1(string input)
        {
            var lines = ParseInput(input);

            var dotList = Fold(lines, true);

            return dotList.Count();
        }

        public string ProcessPart2(string input)
        {
            var lines = ParseInput(input);

            var dotList = Fold(lines);

            var dots = dotList.Select(d => new
            {
                X = int.Parse(d.Split(',')[0]),
                Y = int.Parse(d.Split(',')[1])
            });
            var maxX = dots.Max(d => d.X);
            var maxY = dots.Max(d => d.Y);

            int[,] dotsMap = new int[maxX+1, maxY+1];
            foreach(var dot in dots)
            {
                dotsMap[dot.X, dot.Y] = 1;
            }

            StringBuilder sb = new();
            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    sb.Append(dotsMap[x, y] == 0 ? "." : "#");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public void Process()
        {
            Console.WriteLine("Day 13");

            var input = ReadFile();
            Console.WriteLine($" - Part 1 = {ProcessPart1(input)}");
            Console.WriteLine($" - Part 2 = \n{ProcessPart2(input)}");
        }
    }
}

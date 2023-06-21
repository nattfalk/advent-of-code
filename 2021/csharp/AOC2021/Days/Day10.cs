using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021.Days
{
    public class Day10
    {
        private Dictionary<char, int> charScore = new Dictionary<char, int> {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 },
        };
        private Dictionary<char, long> charScore2 = new Dictionary<char, long> {
            { ')', 1 },
            { ']', 2 },
            { '}', 3 },
            { '>', 4 },
        };

        private char[] openers = new char[] { '(', '{', '[', '<' };
        private Dictionary<char, char> matchingChar = new Dictionary<char, char>()
        {
            { '(', ')' },
            { '{', '}' },
            { '[', ']' },
            { '<', '>' },
            { ')', '(' },
            { '}', '{' },
            { ']', '[' },
            { '>', '<' },
        };

        private string ReadFile()
        {
            return File.ReadAllText(@"..\..\..\days\inputs\day10.txt");
        }

        private string[] ParseInput(string input)
        {
            return input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
        }

        public int ProcessPart1(string input)
        {
            string[] lines = ParseInput(input);

            int score = 0;
            foreach(var line in lines)
            {
                Stack<char> stack = new();
                foreach(var @char in line)
                {
                    if (openers.Contains(@char))
                        stack.Push(@char);
                    else
                    {
                        var c = stack.Pop();
                        var closer = matchingChar[c];
                        if (closer != @char)
                        {
                            score += charScore[@char];
                            break;
                        }
                    }
                }
            }

            return score;
        }

        public long ProcessPart2(string input)
        {
            string[] lines = ParseInput(input);

            List<string> incompleteLines = new();
            foreach (var line in lines)
            {
                var corrupt = false;
                Stack<char> stack = new();
                foreach (var @char in line)
                {
                    if (openers.Contains(@char))
                        stack.Push(@char);
                    else
                    {
                        var closer = matchingChar[stack.Pop()];
                        if (closer != @char)
                        {
                            corrupt = true;
                            break;
                        }
                    }
                }

                if (!corrupt) incompleteLines.Add(line);
            }

            List<long> scores = new();
            foreach (var line in incompleteLines)
            {
                Stack<char> stack = new();
                foreach (var @char in line)
                {
                    if (openers.Contains(@char))
                        stack.Push(@char);
                    else
                        stack.Pop();
                }

                if (stack.Count == 0) continue;

                long cScore = 0;
                do
                {
                    var c = stack.Pop();
                    cScore = (cScore * 5) + charScore2[matchingChar[c]];
                } while (stack.Count > 0);
                scores.Add(cScore);
            }

            return scores.OrderBy(x => x).ToArray()[scores.Count / 2];
        }

        public void Process()
        {
            Console.WriteLine("Day 10");

            var input1 = ReadFile();
            Console.WriteLine($" - Part 1 = {ProcessPart1(input1)}");
            Console.WriteLine($" - Part 2 = {ProcessPart2(input1)}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021.Days
{
    public class Day8
    {
        private string ReadFile()
        {
            return File.ReadAllText(@"..\..\..\days\inputs\day8.txt");
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

            var validLengths = new int[] { 2, 3, 4, 7 };
            var count = 0;
            foreach (var line in lines)
            {
                var fourDigit = line
                    .Split('|')[1]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
                fourDigit.ForEach(x =>
                {
                    if (validLengths.Contains(x.Length)) count++;
                });
            }

            return count;
        }

        public int ProcessPart2(string input)
        {
            string[] lines = ParseInput(input);

            int sum = 0;
            foreach(var line in lines)
            {
                var parts = line.Split(new char[] { ' ', '|' }, StringSplitOptions.RemoveEmptyEntries);

                var digitMap = new string[10];
                digitMap[1] = parts.First(x => x.Length == 2);
                digitMap[4] = parts.First(x => x.Length == 4);
                digitMap[7] = parts.First(x => x.Length == 3);
                digitMap[8] = parts.First(x => x.Length == 7);

                digitMap[2] = parts.Where(p => p.Length == 5 && !digitMap.Contains(p) && p.Intersect(digitMap[4]).Count() == 2).First();
                digitMap[3] = parts.Where(p => p.Length == 5 && !digitMap.Contains(p) && p.Intersect(digitMap[2]).Count() == 4).First();
                digitMap[5] = parts.Where(p => p.Length == 5 && !digitMap.Contains(p)).First();

                digitMap[9] = parts.Where(p => p.Length == 6 && !digitMap.Contains(p) && p.Intersect(digitMap[3]).Count() == 5).First();
                digitMap[6] = parts.Where(p => p.Length == 6 && !digitMap.Contains(p) && p.Intersect(digitMap[7]).Count() == 2).First();
                digitMap[0] = parts.Where(p => p.Length == 6 && !digitMap.Contains(p)).First();

                string Sort(string str) => string.Concat(str.OrderBy(c => c));
                var indexed = digitMap
                    .Select((x, i) => new { x, i })
                    .ToDictionary(x => Sort(x.x), x => x.i);

                parts = line.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                int partSum = 0;
                foreach(var part in parts)
                {
                    partSum = (partSum * 10) + indexed[Sort(part)];
                }

                sum += partSum;
            }

            return sum;
        }

        public void Process()
        {
            Console.WriteLine("Day 8");

            var input1 = ReadFile();
            Console.WriteLine($" - Part 1 = {ProcessPart1(input1)}");
            Console.WriteLine($" - Part 2 = {ProcessPart2(input1)}");
        }
    }
}

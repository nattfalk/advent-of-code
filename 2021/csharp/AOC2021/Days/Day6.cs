using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021.Days
{
    public class Day6
    {
        private string ReadFile()
        {
            var lines = File
                .ReadAllText(@"..\..\..\days\inputs\day6.txt");
            return lines;
        }

        private List<int> ParseInput(string input)
        {
            return input
                .Split(',')
                .Select(x => int.Parse(x))
                .ToList();
        }

        private long GetFishCount(string input, int count)
        {
            List<int> initialData = ParseInput(input);

            long[] fishes = new long[9];
            initialData.ForEach(x => fishes[x]++);

            do
            {
                long newFish = 0;
                for (var j = 0; j < 8; j++)
                {
                    if (j == 0 && fishes[j] > 0)
                        newFish = fishes[0];
                    fishes[j] = fishes[j + 1];
                }
                fishes[6] += newFish;
                fishes[8] = newFish;
            } while (--count > 0);

            return fishes.Sum();
        }

        public long ProcessPart1(string input)
        {
            return GetFishCount(input, 80);
        }

        public long ProcessPart2(string input)
        {
            return GetFishCount(input, 256);
        }

        public void Process()
        {
            Console.WriteLine("Day 6");

            var input1 = ReadFile();
            Console.WriteLine($" - Part 1 = {ProcessPart1(input1)}");
            Console.WriteLine($" - Part 2 = {ProcessPart2(input1)}");
        }
    }
}

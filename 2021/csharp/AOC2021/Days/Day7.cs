using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021.Days
{
    public class Day7
    {
        private string ReadFile()
        {
            var value = File.ReadAllText(@"..\..\..\days\inputs\day7.txt");
            return value;
        }

        private int[] ParseInput(string input)
        {
            return input
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToArray();
        }

        public int ProcessPart1(string input)
        {
            int[] values = ParseInput(input);

            var min = values.Min();
            var max = values.Max();

            var optimalCost = int.MaxValue;
            for (var i=min; i<=max; i++)
            {
                optimalCost = Math.Min(
                    optimalCost,
                    values.Select(x => Math.Abs(x - i)).Sum());
            }

            return optimalCost;
        }

        public int ProcessPart2(string input)
        {
            int[] values = ParseInput(input);

            var min = values.Min();
            var max = values.Max();

            var optimalCost = int.MaxValue;
            for (var i = min; i <= max; i++)
            {
                optimalCost = Math.Min(
                    optimalCost,
                    values.Select(x => GetDistance(Math.Abs(x - i))).Sum());
            }

            return optimalCost;

            int GetDistance(int input)
            {
                return (input * (input + 1)) / 2;
            }
        }

        public void Process()
        {
            Console.WriteLine("Day 7");

            var input1 = ReadFile();
            Console.WriteLine($" - Part 1 = {ProcessPart1(input1)}");
            Console.WriteLine($" - Part 2 = {ProcessPart2(input1)}");
        }
    }
}

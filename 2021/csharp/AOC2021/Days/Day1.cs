using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021.Days
{
    public class Day1
    {
        private int[] ReadFile()
        {
            var lines = File
                .ReadAllLines(@"..\..\..\days\inputs\day1.txt")
                .Select(x => int.Parse(x))
                .ToArray();
            return lines;
        }

        public int ProcessPart1(int[] input)
        {
            var increases = input
                .Where((x, i) => i < input.Count() - 1 && x < input[i + 1])
                .Count();
            return increases;
        }

        public int ProcessPart2(int[] input)
        {
            var increases = input
                .Where((x, i) => 
                    i < input.Count() - 3 && 
                    input.Skip(i).Take(3).Sum() < input.Skip(i+1).Take(3).Sum())
                .Count();
            return increases;
        }

        public void Process()
        {
            Console.WriteLine("Day 1");

            var input1 = ReadFile();
            Console.WriteLine($" - Part 1 = {ProcessPart1(input1)}");
            Console.WriteLine($" - Part 2 = {ProcessPart2(input1)}");
        }
    }
}

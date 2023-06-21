using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021.Days
{
    public class Day2
    {
        private string[] ReadFile()
        {
            var lines = File
                .ReadAllLines(@"..\..\..\days\inputs\day2.txt");
            return lines;
        }

        public int ProcessPart1(string[] input)
        {
            int horizontalPos = 0;
            int depth = 0;
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                int units = int.Parse(parts[1]);
                switch (parts[0])
                {
                    case "forward":
                        horizontalPos += units;
                        break;
                    case "up":
                        depth -= units;
                        break;
                    case "down":
                        depth += units;
                        break;
                }
            }

            return horizontalPos * depth;
        }

        public int ProcessPart2(string[] input)
        {
            int horizontalPos = 0;
            int depth = 0;
            int aim = 0;
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                int units = int.Parse(parts[1]);
                switch (parts[0])
                {
                    case "forward":
                        horizontalPos += units;
                        depth += aim * units;
                        break;
                    case "up":
                        aim -= units;
                        break;
                    case "down":
                        aim += units;
                        break;
                }
            }

            return horizontalPos * depth;
        }

        public void Process()
        {
            Console.WriteLine("Day 2");

            var input1 = ReadFile();
            Console.WriteLine($" - Part 1 = {ProcessPart1(input1)}");
            Console.WriteLine($" - Part 2 = {ProcessPart2(input1)}");
        }
    }
}

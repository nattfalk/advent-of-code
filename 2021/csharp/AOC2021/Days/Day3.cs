using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021.Days
{
    public class Day3
    {
        private string[] ReadFile()
        {
            var lines = File
                .ReadAllLines(@"..\..\..\days\inputs\day3.txt");
            return lines;
        }

        public int ProcessPart1(string[] input)
        {
            var gammaRate = 0;
            var epsilon = 0;
            for (int i=0; i<input[0].Length; i++)
            {
                var mostCommon = input.Select(x => x[i] == '0' ? 0 : 1)
                    .GroupBy(x => x)
                    .OrderByDescending(x => x.Count())
                    .Select(x => x.Key)
                    .First();

                gammaRate = (gammaRate << 1) + mostCommon;
                epsilon = (epsilon << 1) + (mostCommon == 0 ? 1 : 0);
            }

            return gammaRate * epsilon;
        }

        public int ProcessPart2(string[] input)
        {
            var filteredInput = input;
            for (int i = 0; i < input[0].Length; i++)
            {
                var zeros = filteredInput.Where(x => x[i] == '0').ToArray();
                var ones = filteredInput.Where(x => x[i] == '1').ToArray();

                filteredInput = (zeros.Length > ones.Length) ? zeros : ones;
            }
            var oxygenGeneratorRating = Convert.ToInt32(filteredInput.First(), 2);

            filteredInput = input;
            for (int i = 0; i < input[0].Length; i++)
            {
                if (filteredInput.Length == 1) break;

                var zeros = filteredInput.Where(x => x[i] == '0').ToArray();
                var ones = filteredInput.Where(x => x[i] == '1').ToArray();

                filteredInput = (zeros.Length <= ones.Length) ? zeros : ones;
            }
            var co2ScrubberRating = Convert.ToInt32(filteredInput.First(), 2);

            return oxygenGeneratorRating * co2ScrubberRating;
        }

        public void Process()
        {
            Console.WriteLine("Day 3");

            var input1 = ReadFile();
            Console.WriteLine($" - Part 1 = {ProcessPart1(input1)}");
            Console.WriteLine($" - Part 2 = {ProcessPart2(input1)}");
        }
    }
}

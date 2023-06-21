using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021.Days
{
    public class Day11
    {
        private int width;
        private int height;

        private string ReadFile()
        {
            return File.ReadAllText(@"..\..\..\days\inputs\day11.txt");
        }

        private int[,] ParseInput(string input)
        {
            string[] lines = input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            height = lines.Length;
            width = lines[0].Length;
            int[,] result = new int[height, width];
            for(var i=0; i<height; i++)
            {
                var line = lines[i];
                for (var j = 0; j < width; j++)
                    result[i, j] = int.Parse(line[j].ToString());
            }
            return result;
        }

        private void RaiseAdjacent(int[,] energies, Queue<Tuple<int, int>> flashQueue, int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
                return;
            if (energies[y, x] < 10)
            {
                energies[y, x]++;
                if (energies[y, x] == 10)
                    flashQueue.Enqueue(new Tuple<int, int>(x, y));
            }
        }

        private int RaiseEnergy(int[,] energies)
        {
            var flashCount = 0;
            Queue<Tuple<int, int>> flashQueue = new();
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    if (energies[y, x] < 10)
                        energies[y, x]++;
                    if (energies[y, x] == 10)
                        flashQueue.Enqueue(new Tuple<int, int>(x, y));
                }

            if (flashQueue.Count == 0)
                return 0;

            do
            {
                var idx = flashQueue.Dequeue();
                flashCount++;

                RaiseAdjacent(energies, flashQueue, idx.Item1 - 1, idx.Item2 - 1);
                RaiseAdjacent(energies, flashQueue, idx.Item1, idx.Item2 - 1);
                RaiseAdjacent(energies, flashQueue, idx.Item1 + 1, idx.Item2 - 1);
                RaiseAdjacent(energies, flashQueue, idx.Item1 + 1, idx.Item2);
                RaiseAdjacent(energies, flashQueue, idx.Item1 + 1, idx.Item2 + 1);
                RaiseAdjacent(energies, flashQueue, idx.Item1, idx.Item2 + 1);
                RaiseAdjacent(energies, flashQueue, idx.Item1 - 1, idx.Item2 + 1);
                RaiseAdjacent(energies, flashQueue, idx.Item1 - 1, idx.Item2);
            } while (flashQueue.Count > 0);

            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                    if (energies[y, x] >= 10) energies[y, x] = 0;

            return flashCount;
        }

        public (int, string) ProcessPart1(string input, int steps)
        {
            int[,] energies = ParseInput(input);

            var flashCount = 0;
            for (var i = 0; i < steps; i++)
            {
                flashCount += RaiseEnergy(energies);
            }

            StringBuilder sb = new StringBuilder();
            for (var y = 0; y < height; y++)
            {
                if (y>0) sb.AppendLine();
                for (var x = 0; x < width; x++)
                {
                    sb.Append(energies[y, x].ToString());
                }
            }

            return (flashCount, sb.ToString());
        }

        public int ProcessPart2(string input)
        {
            int[,] energies = ParseInput(input);

            int stepCount = 0;
            int flashCount;
            do
            {
                RaiseEnergy(energies);
                stepCount++;

                flashCount = 0;
                for (var i = 0; i < height; i++)
                    for (var j = 0; j < width; j++)
                        if (energies[i, j] == 0) flashCount++;
            } while (flashCount < 100 && stepCount < 1000);

            return stepCount;
        }

        public void Process()
        {
            Console.WriteLine("Day 11");

            var input1 = ReadFile();
            (int result, _) = ProcessPart1(input1, 100);
            Console.WriteLine($" - Part 1 = {result}");
            result = ProcessPart2(input1);
            Console.WriteLine($" - Part 2 = {result}");
        }
    }
}

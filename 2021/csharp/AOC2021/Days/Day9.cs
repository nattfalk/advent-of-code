using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021.Days
{
    public class Day9
    {
        private string ReadFile()
        {
            return File.ReadAllText(@"..\..\..\days\inputs\day9.txt");
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

            var w = lines[0].Length;
            var h = lines.Length;

            int sum = 0;
            for (var i=0; i<w; i++)
                for (var j=0; j<h; j++)
                {
                    var l = i > 0 ? int.Parse(lines[j][i - 1].ToString()) : int.MaxValue;
                    var t = j > 0 ? int.Parse(lines[j - 1][i].ToString()) : int.MaxValue;
                    var r = i < (w - 1) ? int.Parse(lines[j][i + 1].ToString()) : int.MaxValue;
                    var b = j < (h - 1) ? int.Parse(lines[j + 1][i].ToString()) : int.MaxValue;

                    var v = int.Parse(lines[j][i].ToString());

                    if (v < l && v < t && v < r && v < b)
                        sum += v + 1;
                }

            return sum;
        }

        private int Iterate(int?[,] values, int x, int y, int w, int h, int count)
        {
            if (x < 0 || x >= w || y < 0 || y >= h) 
                return count;

            if (!values[y,x].HasValue)
                return count;
            
            values[y,x] = null;

            count = Iterate(values, x, y - 1, w, h, count);
            count = Iterate(values, x + 1, y, w, h, count);
            count = Iterate(values, x, y + 1, w, h, count);
            count = Iterate(values, x - 1, y, w, h, count);

            return count + 1;
        }

        public int ProcessPart2(string input)
        {
            input = input.Replace("9", " ");
            string[] lines = ParseInput(input);

            var w = lines[0].Length;
            var h = lines.Length;

            int?[,] values = new int?[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    if (lines[i][j] != ' ')
                        values[i, j] = int.Parse(lines[i][j].ToString());
                }

            List<int> basins = new();
            for (var y = 0; y < h; y++)
                for (var x = 0; x < w; x++)
                {
                    if (values[y, x].HasValue)
                    {
                        var count = Iterate(values, x, y, w, h, 0);
                        if (count > 0) basins.Add(count);
                    }
                }

            return basins
                .OrderByDescending(x => x)
                .Take(3)
                .Aggregate((x, n) => x*n);
        }

        public void Process()
        {
            Console.WriteLine("Day 9");

            var input1 = ReadFile();
            Console.WriteLine($" - Part 1 = {ProcessPart1(input1)}");
            Console.WriteLine($" - Part 2 = {ProcessPart2(input1)}");
        }
    }
}

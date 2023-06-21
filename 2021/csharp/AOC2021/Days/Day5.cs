using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021.Days
{
    public class Day5
    {
        private struct Line
        {
            public int X1;
            public int Y1;
            public int X2;
            public int Y2;
        }

        private int[,] matrix;

        private string[] ReadFile()
        {
            var lines = File
                .ReadAllLines(@"..\..\..\days\inputs\day5.txt")
                .ToArray();
            return lines;
        }

        private void MarkLine(Line line)
        {
            if (line.X2 < line.X1)
                (line.X1, line.X2) = (line.X2, line.X1);
            else if (line.Y2 < line.Y1)
                (line.Y1, line.Y2) = (line.Y2, line.Y1);

            if (line.X1 != line.X2)
            {
                for(var x = line.X1; x <= line.X2; x++)
                {
                    matrix[x, line.Y1]++;
                }
            }
            else
            {
                for (var y = line.Y1; y <= line.Y2; y++)
                {
                    matrix[line.X1, y]++;
                }
            }
        }

        private void MarkLine2(Line line)
        {
            if (line.Y2 < line.Y1)
            {
                (line.X1, line.X2) = (line.X2, line.X1);
                (line.Y1, line.Y2) = (line.Y2, line.Y1);
            }

            var xsize = Math.Abs(line.X2 - line.X1);
            var ysize = Math.Abs(line.Y2 - line.Y1);

            int xstep = 0;
            int ystep = 0;
            if (xsize >= ysize)
            {
                xstep = (line.X2 - line.X1) / xsize;
                ystep = (line.Y2 - line.Y1) / xsize;
            }
            else if (ysize > xsize)
            {
                xstep = (line.X2 - line.X1) / ysize;
                ystep = (line.Y2 - line.Y1) / ysize;
            }

            var x = line.X1;
            var y = line.Y1;

            for (var i = 0; i <= Math.Max(xsize, ysize); i++)
                matrix[x + (xstep * i), y + (ystep * i)]++;
        }

        public int ProcessPart1(string[] input)
        {
            List<Line> lines = new();

            foreach(string line in input)
            {
                var parts = line.Replace(" -> ", ",").Split(',');
                var l = new Line
                {
                    X1 = int.Parse(parts[0]),
                    Y1 = int.Parse(parts[1]),
                    X2 = int.Parse(parts[2]),
                    Y2 = int.Parse(parts[3])
                };

                if (l.X1 == l.X2 || l.Y1 == l.Y2)
                    lines.Add(l);
            }

            int maxX = lines.Select(l => Math.Max(l.X1, l.X2)).Max();
            int maxY = lines.Select(l => Math.Max(l.Y1, l.Y2)).Max();

            matrix = new int[maxX+1,maxY+1];
            lines.ForEach(l => MarkLine(l));

            var moreThanOne = 0;
            for (var x = 0; x <= maxX; x++)
                for (var y = 0; y <= maxY; y++)
                    moreThanOne += matrix[x, y] > 1 ? 1 : 0;

            return moreThanOne;
        }

        public int ProcessPart2(string[] input)
        {
            List<Line> lines = new();

            foreach (string line in input)
            {
                var parts = line.Replace(" -> ", ",").Split(',');
                var l = new Line
                {
                    X1 = int.Parse(parts[0]),
                    Y1 = int.Parse(parts[1]),
                    X2 = int.Parse(parts[2]),
                    Y2 = int.Parse(parts[3])
                };

                if (l.X1 == l.X2 || l.Y1 == l.Y2
                    || Math.Abs(l.X2 - l.X1) == Math.Abs(l.Y2 - l.Y1))
                    lines.Add(l);
            }

            int maxX = lines.Select(l => Math.Max(l.X1, l.X2)).Max();
            int maxY = lines.Select(l => Math.Max(l.Y1, l.Y2)).Max();

            matrix = new int[maxX + 1, maxY + 1];
            lines.ForEach(l => MarkLine2(l));

            var moreThanOne = 0;
            for (var x = 0; x <= maxX; x++)
                for (var y = 0; y <= maxY; y++)
                    moreThanOne += matrix[x, y] > 1 ? 1 : 0;

            return moreThanOne;
        }

        public void Process()
        {
            Console.WriteLine("Day 5");

            var input1 = ReadFile();
            Console.WriteLine($" - Part 1 = {ProcessPart1(input1)}");
            Console.WriteLine($" - Part 2 = {ProcessPart2(input1)}");
        }
    }
}

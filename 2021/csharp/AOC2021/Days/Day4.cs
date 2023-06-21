using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021.Days
{
    public class Day4
    {
        private class BingoItem
        {
            public int Value;
            public bool Checked;
        }

        private class BingoBoard
        {
            private List<BingoItem> HorizontalLines = new();
            private List<BingoItem> VerticalLines = new();
            public bool Done = false;

            public BingoBoard(BingoItem[] items)
            {
                HorizontalLines.AddRange(items);

                for(var i = 0; i < 5; i++)
                {
                    VerticalLines.AddRange(items.Where((x, idx) => idx % 5 == i));
                }
            }

            public bool HasFullRow(int value)
            {
                for(var i = 0; i < 5; i++)
                {
                    var row = HorizontalLines.Skip(i * 5).Take(5);
                    if (row.Where(x => x.Checked).Count() == 5 &&
                        row.Any(x => x.Value == value))
                    {
                        return true;
                    }

                    row = VerticalLines.Skip(i * 5).Take(5);
                    if (row.Where(x => x.Checked).Count() == 5 &&
                        row.Any(x => x.Value == value))
                    {
                        return true;
                    }
                }

                return false;
            }

            public void Check(int value)
            {
                HorizontalLines
                    .Where(x => x.Value == value)
                    .ToList()
                    .ForEach(x => x.Checked = true);
                VerticalLines
                    .Where(x => x.Value == value)
                    .ToList()
                    .ForEach(x => x.Checked = true);
            }

            public int GetUncheckedSum()
            {
                return HorizontalLines
                    .Where(x => !x.Checked)
                    .Select(x => x.Value)
                    .Sum();
            }
        }

        private int[] values;
        private List<BingoItem> items;
        private int lineCount;

        private List<BingoBoard> boards = new();

        private string[] ReadFile()
        {
            var lines = File
                .ReadAllLines(@"..\..\..\days\inputs\day4.txt");
            return lines;
        }

        public int ProcessPart1(string[] input)
        {
            values = input[0].Split(',').Select(x => int.Parse(x)).ToArray();

            items = new();
            foreach (var line in input.Skip(2))
            {
                line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                    .ForEach(x => items.Add(new BingoItem { Value = int.Parse(x), Checked = false }));
            }

            lineCount = items.Count() / 5;

            foreach (var value in values)
            {
                items
                    .Where(x => x.Value == value)
                    .ToList()
                    .ForEach(x => x.Checked = true);

                for (var i = 0; i < lineCount; i++)
                {
                    var row = items.Skip(i * 5).Take(5);
                    var isNotFullRow = row.Where(x => !x.Checked).Any();
                    if (isNotFullRow) continue;

                    var boardIndex = i / 5;

                    var result = items
                        .Skip(boardIndex * 25)
                        .Take(25)
                        .Where(x => !x.Checked)
                        .Sum(x => x.Value) * value;
                    return result;
                }
            }

            return 0;
        }

        public int ProcessPart2(string[] input)
        {
            values = input[0].Split(',').Select(x => int.Parse(x)).ToArray();

            items = new();
            foreach (var line in input.Skip(2))
            {
                line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                    .ForEach(x => items.Add(new BingoItem
                    {
                        Value = int.Parse(x),
                        Checked = false
                    }));
            }
            foreach (var chunk in items.Chunk(25))
            {
                boards.Add(new BingoBoard(chunk));
            }

            lineCount = items.Count() / 5;

            var winCount = 0;
            foreach (var value in values)
            {
                var activeBoards = boards.Where(b => !b.Done).ToList();
                activeBoards.ForEach(b => b.Check(value));
                foreach (var board in activeBoards)
                {
                    if (board.HasFullRow(value))
                    {
                        board.Done = true;
                        winCount++;
                        if (winCount == boards.Count())
                        {
                            return board.GetUncheckedSum() * value;
                        }
                    }
                }
            }
            return 0;
        }

        public void Process()
        {
            Console.WriteLine("Day 4");

            var input1 = ReadFile();
            Console.WriteLine($" - Part 1 = {ProcessPart1(input1)}");
            Console.WriteLine($" - Part 2 = {ProcessPart2(input1)}");
        }
    }
}

using AOC2021.Days;

using Xunit;

namespace AOC2021.Test
{
    public class DayTests
    {
        [Fact]
        public void Day01_part1_ReturnsExpected()
        {
            var day = new Day1();

            var input = new int[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
            var result = day.ProcessPart1(input);

            Assert.Equal(7, result);
        }

        [Fact]
        public void Day01_part2_ReturnsExpected()
        {
            var day = new Day1();

            var input = new int[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
            var result = day.ProcessPart2(input);

            Assert.Equal(5, result);
        }

        [Fact]
        public void Day02_part1_ReturnsExpected()
        {
            var day = new Day2();

            var input = new string[] {
                "forward 5",
                "down 5",
                "forward 8",
                "up 3",
                "down 8",
                "forward 2"
            };
            var result = day.ProcessPart1(input);

            Assert.Equal(150, result);
        }

        [Fact]
        public void Day02_part2_ReturnsExpected()
        {
            var day = new Day2();

            var input = new string[] {
                "forward 5",
                "down 5",
                "forward 8",
                "up 3",
                "down 8",
                "forward 2"
            };
            var result = day.ProcessPart2(input);

            Assert.Equal(900, result);
        }

        [Fact]
        public void Day03_part1_ReturnsExpected()
        {
            var day = new Day3();

            var input = new string[] {
                "00100","11110","10110","10111",
                "10101","01111","00111","11100",
                "10000","11001","00010","01010"
            };
            var result = day.ProcessPart1(input);

            Assert.Equal(198, result);
        }

        [Fact]
        public void Day03_part2_ReturnsExpected()
        {
            var day = new Day3();

            var input = new string[] {
                "00100","11110","10110","10111",
                "10101","01111","00111","11100",
                "10000","11001","00010","01010"
            };
            var result = day.ProcessPart2(input);

            Assert.Equal(230, result);
        }

        [Fact]
        public void Day04_part1_ReturnsExpected()
        {
            var day = new Day4();

            var input = new string[] {
                "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1",
                "",
                "22 13 17 11  0",
                " 8  2 23  4 24",
                "21  9 14 16  7",
                " 6 10  3 18  5",
                " 1 12 20 15 19",
                "",
                " 3 15  0  2 22",
                " 9 18 13 17  5",
                "19  8  7 25 23",
                "20 11 10 24  4",
                "14 21 16 12  6",
                "",
                "14 21 17 24  4",
                "10 16 15  9 19",
                "18  8 23 26 20",
                "22 11 13  6  5",
                " 2  0 12  3  7"
            };
            var result = day.ProcessPart1(input);

            Assert.Equal(4512, result);
        }

        [Fact]
        public void Day04_part2_ReturnsExpected()
        {
            var day = new Day4();

            var input = new string[] {
                "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1",
                "",
                "22 13 17 11  0",
                " 8  2 23  4 24",
                "21  9 14 16  7",
                " 6 10  3 18  5",
                " 1 12 20 15 19",
                "",
                " 3 15  0  2 22",
                " 9 18 13 17  5",
                "19  8  7 25 23",
                "20 11 10 24  4",
                "14 21 16 12  6",
                "",
                "14 21 17 24  4",
                "10 16 15  9 19",
                "18  8 23 26 20",
                "22 11 13  6  5",
                " 2  0 12  3  7"
            };
            var result = day.ProcessPart2(input);

            Assert.Equal(1924, result);
        }

        [Fact]
        public void Day05_part1_ReturnsExpected()
        {
            var day = new Day5();

            var input = new string[] {
                "0,9 -> 5,9",
                "8,0 -> 0,8",
                "9,4 -> 3,4",
                "2,2 -> 2,1",
                "7,0 -> 7,4",
                "6,4 -> 2,0",
                "0,9 -> 2,9",
                "3,4 -> 1,4",
                "0,0 -> 8,8",
                "5,5 -> 8,2"
            };
            var result = day.ProcessPart1(input);

            Assert.Equal(5, result);
        }

        [Fact]
        public void Day05_part2_ReturnsExpected()
        {
            var day = new Day5();

            var input = new string[] {
                "0,9 -> 5,9",
                "8,0 -> 0,8",
                "9,4 -> 3,4",
                "2,2 -> 2,1",
                "7,0 -> 7,4",
                "6,4 -> 2,0",
                "0,9 -> 2,9",
                "3,4 -> 1,4",
                "0,0 -> 8,8",
                "5,5 -> 8,2"
            };
            var result = day.ProcessPart2(input);

            Assert.Equal(12, result);
        }

        [Fact]
        public void Day06_part1_ReturnsExpected()
        {
            var day = new Day6();

            var input = "3,4,3,1,2";
            var result = day.ProcessPart1(input);

            Assert.Equal(5934, result);
        }

        [Fact]
        public void Day06_part2_ReturnsExpected()
        {
            var day = new Day6();

            var input = "3,4,3,1,2";
            var result = day.ProcessPart2(input);

            Assert.Equal(26984457539, result);
        }

        [Fact]
        public void Day07_ReturnsExpected()
        {
            var day = new Day7();

            var input = "16,1,2,0,4,2,7,1,2,14";
            var part1 = day.ProcessPart1(input);
            var part2 = day.ProcessPart2(input);

            Assert.Equal(37, part1);
            Assert.Equal(168, part2);
        }

        [Fact]
        public void Day08_ReturnsExpected()
        {
            var input = @"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce";

            var day = new Day8();
            var part1 = day.ProcessPart1(input);

            var input2 = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf";
            var part2 = day.ProcessPart2(input2);

            var part22 = day.ProcessPart2(input);

            Assert.Equal(26, part1);
            Assert.Equal(5353, part2);
            Assert.Equal(61229, part22);
        }

        [Fact]
        public void Day09_ReturnsExpected()
        {
            var input = @"2199943210
3987894921
9856789892
8767896789
9899965678";

            var day = new Day9();
            var part1 = day.ProcessPart1(input);
            var part2 = day.ProcessPart2(input);

            Assert.Equal(15, part1);
            Assert.Equal(1134, part2);
        }

        [Fact]
        public void Day10_ReturnsExpected()
        {
            var input = @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]";

            var day = new Day10();
            var part1 = day.ProcessPart1(input);
            var part2 = day.ProcessPart2(input);

            Assert.Equal(26397, part1);
            Assert.Equal(288957, part2);
        }

        [Fact]
        public void Day11_AfterStep1_ReturnsExpected()
        {
            var input = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";

            var day = new Day11();
            (int part1, string energies) = day.ProcessPart1(input, 10);

            Assert.Equal(204, part1);
            Assert.Equal(@"0481112976
0031112009
0041112504
0081111406
0099111306
0093511233
0442361130
5532252350
0532250600
0032240000", energies);

            int part2 = day.ProcessPart2(input);
            Assert.Equal(195, part2);
        }

        [Trait("Category", "Day 12")]
        [Fact]
        public void Day12_ParseInput_ReturnCorrectNodelist()
        {
            var input = @"start-A
A-end";

            var day = new Day12();
            var nodes = day.ParseInput(input);

            Assert.Equal(3, nodes.Count);
            Assert.Contains("A", nodes[0].Children);
            Assert.Contains("end", nodes[1].Children);
        }

        [Trait("Category", "Day 12")]
        [Fact]
        public void Day12_AfterStep1_ReturnsExpected()
        {
            var input = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end";

            var day = new Day12();
            var result = day.ProcessPart1(input);

            Assert.Equal(10, result);
        }

        [Trait("Category", "Day 12")]
        [Fact]
        public void Day12_AfterStep2_ReturnsExpected()
        {
            var input = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end";

            var day = new Day12();
            var result = day.ProcessPart2(input);

            Assert.Equal(36, result);
        }

        [Fact]
        public void Day13_AfterStep1_ReturnsExpected()
        {
            var input = @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";

            var day = new Day13();
            var result = day.ProcessPart1(input);

            Assert.Equal(17, result);
        }

        [Fact]
        public void Day14_AfterStep1_ReturnsExpected()
        {
            var input = @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

            var day = new Day14();
            var result = day.ProcessPart1(input);

            Assert.Equal(1588, result);
        }

        [Fact]
        public void Day14_AfterStep2_ReturnsExpected()
        {
            var input = @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

            var day = new Day14();
            var result = day.ProcessPart2(input);

            Assert.Equal(2188189693529, result);
        }
    }
}
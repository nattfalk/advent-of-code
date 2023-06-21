using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021.Days
{
    public class Day14
    {
        private string ReadFile()
        {
            return File.ReadAllText(@"..\..\..\days\inputs\day14.txt");
        }

        public (string, Dictionary<string, string>) ParseInput(string input)
        {
            string[] lines = input
                .Split(Environment.NewLine)
                .ToArray();

            string template = "";
            Dictionary<string, string> rules = new();

            foreach(var line in lines)
            {
                if (string.IsNullOrEmpty(template))
                {
                    template = line;
                    continue;
                }
                if (string.IsNullOrEmpty(line))
                    continue;

                var parts = line.Split(" -> ");
                rules.Add(parts[0], parts[1]);
            }

            return (template, rules);
        }

        public int ProcessPart1(string input)
        {
            var (template, rules) = ParseInput(input);

            var steps = 10;
            var polymer = template;
            while (steps-- > 0)
            {
                var updatedPolymer = string.Empty;
                for (int i = 0; i < polymer.Length - 1; i++)
                {
                    updatedPolymer += polymer[i];
                    updatedPolymer += rules[polymer.Substring(i, 2)];
                }
                updatedPolymer += polymer[polymer.Length - 1];
                polymer = updatedPolymer;
            };

            var groupedElements = polymer.ToCharArray()
                .GroupBy(c => c)
                .Select(c => new
                {
                    Element = c.Key,
                    Count = c.Count()
                });
            var min = groupedElements.Min(x => x.Count);
            var max = groupedElements.Max(x => x.Count);

            return max - min;
        }

        //
        // Brute force, non-working
        //
        //struct StoredElement
        //{
        //    public int Level;
        //    public char Element;
        //}

        //public Dictionary<char, long> CountChars(string input, int steps)
        //{
        //    var (template, rules) = ParseInput(input);

        //    Stack<StoredElement> elements = new();
        //    Dictionary<char, long> result = new();

        //    foreach(var rule in rules)
        //    {
        //        foreach(var c in rule.Key+rule.Value)
        //        {
        //            if (!result.ContainsKey(c)) result.Add(c, 0);
        //        }
        //    }

        //    result[template[0]] = 1;
        //    for(int i=0; i<template.Length-1; i++)
        //    {
        //        elements.Push(new StoredElement { Element = template[i+1], Level = 0 });

        //        var previous = new StoredElement { Element = template[i], Level = 0 };
        //        while(elements.Count > 0)
        //        {
        //            var next = elements.Peek();
        //            var newLevel = Math.Max(previous.Level, next.Level) + 1;
        //            var newElement = rules[$"{previous.Element}{next.Element}"][0];
        //            elements.Push(new StoredElement { Element = newElement, Level = newLevel });

        //            if (newLevel == steps)
        //            {
        //                var e = elements.Pop();
        //                result[e.Element]++;
        //                e = elements.Pop();
        //                result[e.Element]++;
        //                previous = e;
        //            }
        //        }
        //    }

        //    return result;
        //}

        //public long ProcessPart2(string input)
        //{
        //    var result = CountChars(input, 40);

        //    var min = result.Min(x => x.Value);
        //    var max = result.Max(x => x.Value);

        //    return max - min;
        //}

        public long ProcessPart2(string input)
        {
            var (template, rules) = ParseInput(input);

            Dictionary<char, long> countMap = new();
            Dictionary<string, long> pairCountMap = new();

            for (var i = 0; i < template.Length - 1; i++)
                IncreaseCounter(pairCountMap, template.Substring(i, 2), 1);

            foreach (var c in template)
                IncreaseCounter(countMap, c, 1);

            for (var i=0; i<40; i++)
            {
                Dictionary<string, long> newPairCountMap = new();

                foreach(var p in pairCountMap)
                {
                    var newElement = rules[p.Key][0];

                    var e1 = $"{p.Key[0]}{newElement}";
                    IncreaseCounter(newPairCountMap, e1, p.Value);
                    var e2 = $"{newElement}{p.Key[1]}";
                    IncreaseCounter(newPairCountMap, e2, p.Value);

                    IncreaseCounter<char>(countMap, newElement, p.Value);
                }
                pairCountMap = newPairCountMap;
            }

            var min = countMap.Min(c => c.Value);
            var max = countMap.Max(c => c.Value);

            return max - min;

            void IncreaseCounter<T>(Dictionary<T, long> map, T key, long value)
            {
                if (!map.ContainsKey(key)) map.Add(key, 0);
                map[key] += value;
            }
        }

        public void Process()
        {
            Console.WriteLine("Day 14");

            var input = ReadFile();
            Console.WriteLine($" - Part 1 = {ProcessPart1(input)}");
            Console.WriteLine($" - Part 2 = {ProcessPart2(input)}");
        }
    }
}

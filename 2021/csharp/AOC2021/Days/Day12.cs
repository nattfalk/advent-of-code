using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021.Days
{
    public class Day12
    {
        public enum NodeType
        {
            Start,
            End,
            SmallCave,
            LargeCave
        }

        public class Node
        {
            public string Name;
            public NodeType Type;
            public List<string> Children = new();
        }

        public class Path
        {
            public List<Node> Nodes = new();
        }

        private List<IEnumerable<Node>> ValidPaths = new();
        private List<Node> Nodes = new();

        private string ReadFile()
        {
            return File.ReadAllText(@"..\..\..\days\inputs\day12.txt");
        }

        private NodeType GetNodeType(string nodeName)
        {
            return nodeName switch
            {
                "start" => NodeType.Start,
                "end" => NodeType.End,
                _ => Char.IsUpper(nodeName[0]) ? NodeType.LargeCave : NodeType.SmallCave
            };
        }

        public List<Node> ParseInput(string input)
        {
            string[] lines = input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            List<Node> nodes = new();
            foreach(var line in lines)
            {
                var parts = line.Split('-', StringSplitOptions.RemoveEmptyEntries);

                var node1 = nodes.FirstOrDefault(n => n.Name.Equals(parts[0], StringComparison.OrdinalIgnoreCase));
                if (node1 is null)
                {
                    node1 = new Node()
                    {
                        Name = parts[0],
                        Type = GetNodeType(parts[0])
                    };
                }

                var node2 = nodes.FirstOrDefault(n => n.Name.Equals(parts[1], StringComparison.OrdinalIgnoreCase));
                if (node2 is null)
                {
                    node2 = new Node()
                    {
                        Name = parts[1],
                        Type = GetNodeType(parts[1])
                    };
                }

                if (!node1.Children.Contains(node2.Name)) node1.Children.Add(node2.Name);
                if (!node2.Children.Contains(node1.Name)) node2.Children.Add(node1.Name);

                if (!nodes.Contains(node1)) nodes.Add(node1);
                if (!nodes.Contains(node2)) nodes.Add(node2);
            }

            return nodes;
        }

        private void BuildPaths(
            Node currentNode,
            Stack<Node> nodeStack,
            Func<Stack<Node>, Node, bool> validateNode)
        {
            nodeStack.Push(currentNode);

            foreach(var child in currentNode.Children)
            {
                var node = Nodes.First(n => n.Name == child);

                if (!validateNode(nodeStack, node))
                    continue;

                if (node.Type == NodeType.End)
                {
                    var validPath = new List<Node>();
                    foreach(var n in nodeStack.Reverse())
                    {
                        validPath.Add(n);
                    }
                    validPath.Add(node);
                    ValidPaths.Add(validPath);
                    continue;
                }

                BuildPaths(node, nodeStack, validateNode);
            }

            nodeStack.Pop();
        }


        public int ProcessPart1(string input)
        {
            Nodes = ParseInput(input);
            ValidPaths = new();

            var startNode = Nodes.First(n => n.Type == NodeType.Start);
            BuildPaths(
                startNode, 
                new Stack<Node>(),
                CanAddSmallCave);

            return ValidPaths.Count;

            bool CanAddSmallCave(Stack<Node> nodeStack, Node currentNode)
            {
                if ((currentNode.Type == NodeType.Start
                    || currentNode.Type == NodeType.SmallCave)
                    && nodeStack.Contains(currentNode))
                    return false;
                return true;
            }
        }

        public int ProcessPart2(string input)
        {
            Nodes = ParseInput(input);
            ValidPaths = new();

            var startNode = Nodes.First(n => n.Type == NodeType.Start);
            BuildPaths(
                startNode,
                new Stack<Node>(),
                CanAddSmallCave);

            return ValidPaths.Count;

            bool CanAddSmallCave(Stack<Node> nodeStack, Node currentNode)
            {
                if (currentNode.Type == NodeType.Start
                    && nodeStack.Contains(currentNode))
                    return false;

                if (currentNode.Type == NodeType.LargeCave
                    || currentNode.Type == NodeType.End)
                    return true;

                var smallCaveCount = nodeStack
                    .ToList()
                    .Where(n => n.Type == NodeType.SmallCave)
                    .GroupBy(n => n.Name)
                    .Select(ng => new { 
                        Name = ng.Key, 
                        Count = ng.Count() 
                    });
                var maxSmallCaveCount = smallCaveCount
                    .Select(n => n.Count)
                    .DefaultIfEmpty()
                    .Max();
                var currentCaveCount = smallCaveCount
                    .Where(n => n.Name == currentNode.Name)
                    .Select(n => n.Count)
                    .FirstOrDefault(0);

                if (maxSmallCaveCount == 2 && currentCaveCount >= 1)
                    return false;

                return true;
            }
        }

        public void Process()
        {
            Console.WriteLine("Day 12");

            var input = ReadFile();
            Console.WriteLine($" - Part 1 = {ProcessPart1(input)}");
            Console.WriteLine($" - Part 2 = {ProcessPart2(input)}");
        }
    }
}

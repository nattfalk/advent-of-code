// using System.Diagnostics;

// int[,] _map;
// int _sx = 0, _sy = 0;
// int _dx = 0, _dy = 0;
// int _maxX = 0, _maxY = 0;

// List<Node> _queue = new();
// HashSet<Node> _visited = new();

// Stopwatch sw = new();
// sw.Start();
// parseFile();

// Console.WriteLine($"Part 1 : {Part1()}");
// Console.WriteLine($"Part 2 : ");
// sw.Stop();
// Console.WriteLine("Time: " + sw.ElapsedMilliseconds);

// int Part1()
// {
//     var n = new Node(_sx, _sy, 0);
//     _queue.Add(n);

//     while (_queue.Except(_visited).Any())
//     {
//         var node = _queue.Except(_visited).OrderBy(n => n.cost).First();
//         _visited.Add(node);

//         CheckDirection(node,  1,  0);
//         CheckDirection(node,  0, -1);
//         CheckDirection(node,  0,  1);
//         CheckDirection(node, -1,  0);
//     }

//     return _visited.FirstOrDefault(n => n.x == _dx && n.y == _dy)?.cost ?? -1;
// }

// void CheckDirection(Node node, int x, int y)
// {
//     var nx = node.x + x;
//     var ny = node.y + y;

//     if (nx < 0 || nx >= _maxX || ny < 0 || ny >= _maxY)
//         return;

//     if ((_map[ny, nx]-_map[node.y, node.x]) <= 1)
//     {
//         Node? n2 = _queue.FirstOrDefault(n => n.x == node.x + x && n.y == node.y + y);
//         if (n2 is null)
//         {
//             n2 = new Node(nx, ny, int.MaxValue);
//             _queue.Add(n2);
//         }

//         if (node.cost + 1 < n2.cost)
//             n2.cost = node.cost + 1;
//     }
// }

// void parseFile()
// {
//     string input;
//     input = File.ReadAllText(@"..\input.txt");
// //     input = @"Sabqponm
// // abcryxxl
// // accszExk
// // acctuvwj
// // abdefghi";
//     var _lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

//     _maxX = _lines[0].Length;
//     _maxY = _lines.Length;

//     _map = new int[_maxY, _maxX];
//     for(var y=0; y<_maxY; y++)
//         for (var x=0; x<_maxX; x++)
//         {
//             var chr = _lines[y][x];
//             if (Char.IsLower(chr))
//             {
//                 _map[y,x] = _lines[y][x] - 'a';
//             }
//             else if (chr == 'S')
//             {
//                 _map[y,x] = 0;
//                 _sx = x;
//                 _sy = y;
//             }
//             else if (chr == 'E')
//             {
//                 _dx = x;
//                 _dy = y;
//                 _map[y,x] = 'z'-'a';
//             }
//         }
// }

// class Node
// {
//     public Node(int x, int y, int cost)
//     {
//         this.x = x;
//         this.y = y;
//         this.cost = cost;
//     }
//     public int x { get; set; }
//     public int y { get; set; }
//     public int cost { get; set; }
// }
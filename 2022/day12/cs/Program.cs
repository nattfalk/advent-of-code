using System.Diagnostics;

int[,] _map;
int[,] _costs;
Node start = new Node(0,0);
Node end = new Node(0,0);
int _maxX = 0, _maxY = 0;

Queue<Node> _queue = new();
HashSet<Node> _aList = new();

parseFile();

Stopwatch sw = new();
sw.Start();
Console.WriteLine($"Part 1 : {Part1()}");
sw.Stop();
Console.WriteLine($"- Executiontime: {sw.ElapsedMilliseconds}ms");

sw.Reset();
sw.Start();
Console.WriteLine($"Part 2 : {Part2()}");
sw.Stop();
Console.WriteLine($"- Executiontime: {sw.ElapsedMilliseconds}ms");

void CheckDirection(Node node, int x, int y)
{
    Node n = new Node(node.x + x, node.y + y);
    if (n.x < 0 || n.x >= _maxX || n.y < 0 || n.y >= _maxY)
        return;

    if (_map[n.y, n.x]-_map[node.y, node.x] <= 1)
    {
        var newCost = _costs[node.y, node.x] + 1;
        if (_costs[n.y, n.x] > newCost && _costs[n.y, n.x] != -1)
        {
            _costs[n.y, n.x] = newCost;
        }
        else if (_costs[n.y, n.x] == -1)
        {
            _costs[n.y, n.x] = newCost;
            _queue.Enqueue(n);
        }
    }
}

int Part1()
{
    _costs[start.y, start.x] = 0;
    var n = new Node(start.x, start.y);
    _queue.Enqueue(n);

    while (_queue.Any())
    {
        var node = _queue.Dequeue();

        CheckDirection(node,  1,  0);
        CheckDirection(node,  0, -1);
        CheckDirection(node,  0,  1);
        CheckDirection(node, -1,  0);
    }

    return _costs[end.y, end.x];
}

int Part2()
{
    var cost = int.MaxValue;
    foreach (var aNode in _aList)
    {
        // Reset costs
        for(var y=0; y<_maxY; y++)
            for (var x=0; x<_maxX; x++)
                _costs[y,x] = -1;

        _costs[aNode.y, aNode.x] = 0;
        var n = aNode;
        _queue.Clear();
        _queue.Enqueue(n);

        while (_queue.Any())
        {
            var node = _queue.Dequeue();

            CheckDirection(node,  1,  0);
            CheckDirection(node,  0, -1);
            CheckDirection(node,  0,  1);
            CheckDirection(node, -1,  0);
        }

        if (_costs[end.y, end.x] != -1 && _costs[end.y, end.x] < cost)
            cost = _costs[end.y, end.x];
    }

    return cost;
}

void parseFile()
{
    string input;
    input = File.ReadAllText(@"..\input.txt");
//     input = @"Sabqponm
// abcryxxl
// accszExk
// acctuvwj
// abdefghi";
    var _lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

    _maxX = _lines[0].Length;
    _maxY = _lines.Length;

    _map = new int[_maxY, _maxX];
    _costs = new int[_maxY, _maxX];
    for(var y=0; y<_maxY; y++)
        for (var x=0; x<_maxX; x++)
        {
            _costs[y,x] = -1;

            var chr = _lines[y][x];
            if (Char.IsLower(chr))
            {
                if (chr == 'a')
                    _aList.Add(new Node(x, y));
                _map[y,x] = _lines[y][x] - 'a';
            }
            else if (chr == 'S')
            {
                _map[y,x] = 0;
                start = new Node(x, y);
            }
            else if (chr == 'E')
            {
                _map[y,x] = 'z'-'a';
                end = new Node(x, y);
            }
        }
}

struct Node
{
    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public int x { get; }
    public int y { get; }
}
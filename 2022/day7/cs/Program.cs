var _input = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";
string[] _lines;

DirEntry rootDir = new();
List<DirEntry> dirList = new();

parseFile();

var part1 = Part1();
var part2 = Part2();

Console.WriteLine($"Part 1 : {part1}");
Console.WriteLine($"Part 2 : {part2}");

long Part1() =>
    dirList.Where(d => d.Size <= 100000).Sum(d => d.Size);

long Part2() =>
    dirList
        .OrderBy(x => x.Size)
        .First(x => x.Size >= (30000000 - (70000000 - rootDir.Size)))
        .Size;

void parseFile()
{
    _input = File.ReadAllText(@"..\input.txt");
    _lines = _input.Split(Environment.NewLine);

    Stack<DirEntry> dirs = new Stack<DirEntry>();
    dirs.Push(rootDir);
    dirList.Add(rootDir);

    foreach(var line in _lines)
    {
        if (line == "$ cd /")
        {
            while (dirs.Count > 1) dirs.Pop();
        }
        else if (line == "$ cd ..")
        {
            dirs.Pop();
        }
        else if (line.StartsWith("$ cd "))
        {
            var d = dirs.Peek();
            dirs.Push(d.Dirs.First(d => d.Name == line[5..]));
        }
        else if (!line.StartsWith("$"))
        {
            var parts = line.Split(' ');
            if (long.TryParse(parts[0], out long fsize))
            {
                dirs.Peek().Files.Add(new FileEntry(parts[1], fsize));
            }
            else
            {
                var d = new DirEntry() { Name = parts[1] };
                dirList.Add(d);
                dirs.Peek().Dirs.Add(d);
            }
        }
    }
}

public record struct FileEntry(string Filename, long FileSize);

class DirEntry
{
    public string Name { get; set; } = "_";
    public List<DirEntry> Dirs { get; set; } = new();
    public List<FileEntry> Files { get; set; } = new();
    public long Size => Files.Sum(x => x.FileSize) + Dirs.Sum(x => x.Size);
}

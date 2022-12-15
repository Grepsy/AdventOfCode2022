namespace AdventOfCode2022;

public static class Day07 {
    public static object Run1() { // 1232307
        var rootNode = CalcTree();
        return rootNode.Descendents.Where(x => x.TotalSize <= 100000).Sum(x => x.TotalSize);
    }

    public static object Run2() { // 7268994
        var rootNode = CalcTree();
        var diskSize = 70000000;
        var updateSize = 30000000;
        var freeSpace = diskSize - rootNode.TotalSize;
        var requiredSpace = updateSize - freeSpace;

        return rootNode.Descendents.Where(x => x.TotalSize > requiredSpace).Min(x => x.TotalSize);
    }

    private static Node CalcTree() {
        var rootNode = new Node("root", null);
        var node = rootNode;

        foreach (var line in File.ReadLines("day7.txt")) {
            node = line.Split(' ') switch {
                ["$", "cd", ".."] => node.Parent!,
                ["$", "cd", var dir] => new Node(dir, node),
                [var s, string] when int.TryParse(s, out var size) => node.AddSize(size),
                _ => node
            };
        }

        return rootNode;
    }
}

public class Node {
    public string Name { get; }
    public int Size { get; private set; }
    public Node? Parent { get; }
    public List<Node> Children { get; } = new();
    public IEnumerable<Node> Descendents => Children.Concat(Children.SelectMany(x => x.Descendents));
    public int TotalSize => Size + Descendents.Sum(x => x.Size);

    public Node(string name, Node? parent) {
        Name = name;
        Parent = parent;
        parent?.Children.Add(this);
    }

    public Node AddSize(int size) {
        Size += size;
        return this;
    }
}
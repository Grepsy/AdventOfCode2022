namespace AdventOfCode2022;

public static class Day17 {
    public record Shape(Vec[] Blocks, Vec Pos) {
        public IEnumerable<Vec> Bla => Blocks.Select(x => Pos + x);
    }

    public static object Run1() {
        var shapes = new[] {
            new Vec[] { (0, 0), (1, 0), (2, 0), (3, 0) },
            new Vec[] { (1, 0), (0, 1), (1, 1), (2, 1), (1, 2) },
            new Vec[] { (2, 0), (2, 1), (2, 2), (1, 2), (0, 2) },
            new Vec[] { (0, 0), (0, 1), (0, 2), (0, 3) },
            new Vec[] { (0, 0), (1, 0), (0, 1), (1, 1) }
        };

        bool isCollision(Shape shape, bool[,] grid) => shape.Bla.Any(p => grid[p.Y, p.X]);

        var grid = new bool[5000, 9];
        var shapeIndex = 0;

        return 0;
    }

    public static object Run2() {
        return 0;
    }
}
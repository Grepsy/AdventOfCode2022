namespace AdventOfCode2022;

public static class Day12 {
    public static object Run1() {
        var grid = new Grid<char>(File.ReadAllText("day12.txt").Split("\n").Select(x => x.ToCharArray()));
        var start = grid.First(x => x == 'S');
        var end = grid.First(x => x == 'E');
        start.Value = 'a';
        end.Value = 'z';

        return Find(grid, start.Pos, end.Pos, (a, b) => a - b <= 1)!.Count;
    }

    public static object Run2() {
        var grid = new Grid<char>(File.ReadAllText("day12.txt").Split("\n").Select(x => x.ToCharArray()));
        var start = grid.First(x => x == 'S');
        var end = grid.First(x => x == 'E');
        start.Value = 'a';
        end.Value = 'z';

        return grid.Where(x => x == 'a').Min(s => Find(grid, s.Pos, end.Pos, (a, b) => a - b <= 1)?.Count ?? int.MaxValue);
    }

    public static IReadOnlyList<Vec>? Find<T>(Grid<T> grid, Vec start, Vec end, Func<T, T, bool> wallFn) {
        var list = new List<(Vec pos, int count)> { (end, 0) };

        for (var i = 0; i < list.Count && list[i].pos != start; i++) {
            var cell = grid[list[i].pos];
            list.AddRange(from n in cell.Neighbors()
                          where wallFn(cell!, n!) && list.All(x => x.pos != n.Pos)
                          select (n.Pos, list[i].count + 1));
        }

        var next = Grid<int>.FromSparse(list)[start];
        var path = new List<Vec>();
        var failed = false;

        while (!failed && next!.Pos != end) {
            path.Add(next.Pos);
            (next, failed) = next!.Neighbors().Where(x => x == next - 1).ToArray() switch {
                [var x, ..] => (x, false),
                [] => (null, true)
            };
        }

        return failed ? null : path;
    }
}
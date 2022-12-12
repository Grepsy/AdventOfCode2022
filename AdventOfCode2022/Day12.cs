using System.Collections;
using static System.Linq.Enumerable;

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

public class Grid<T> : IEnumerable<Grid<T>> {
    public readonly Vec Pos;
    public readonly T[][] Items;

    public int LengthY => Items.Length;
    public int LengthX => Items[0].Length;
    public bool InRange => Pos.X >= 0 && Pos.X < LengthX && Pos.Y >= 0 && Pos.Y < LengthY;

    public T? Value {
        get => InRange ? Items[Pos.Y][Pos.X] : default;
        set { if (InRange && value != null) { Items[Pos.Y][Pos.X] = value; } }
    }

    public Grid(IEnumerable<IEnumerable<T>> grid) : this(grid.Select(x => x.ToArray()).ToArray(), (0, 0)) { }

    private Grid(T[][] grid, Vec offset) {
        Items = grid;
        Pos = offset;
    }

    public Grid<T> this[int x, int y] => new(Items, Pos + (x, y));
    public Grid<T> this[Vec offset] => new(Items, Pos + offset);
    public static implicit operator T?(Grid<T> grid) => grid.Value;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Grid<T>> GetEnumerator() {
        var grid = this;
        var list = from y in Range(0, grid.LengthY - 1) from x in Range(0, grid.LengthX - 1) select grid[x, y];
        return list.GetEnumerator();
    }

    public IEnumerable<Grid<T>> Neighbors() => new[] { this[-1, 0], this[0, -1], this[1, 0], this[0, 1] }.Where(x => x.InRange);

    public static Grid<T> FromSparse(IEnumerable<(Vec pos, T val)> items) {
        var maxX = items.Max(i => i.pos.X);
        var maxY = items.Max(i => i.pos.Y);

        var array = new T[maxY + 1][];
        for (var y = 0; y < array.Length; y++) {
            array[y] = new T[maxX + 1];
            for (var x = 0; x < array[0].Length; x++) {
                array[y][x] = items.Where(i => i.pos == (x, y)).Select(i => i.val).FirstOrDefault() ?? default;
            }
        }

        return new Grid<T>(array);
    }
}

public readonly record struct Vec(int X, int Y) {
    public static implicit operator Vec((int x, int y) tuple) => new(tuple.x, tuple.y);
    public static Vec operator -(Vec left, Vec right) => new(left.X - right.X, left.Y - right.Y);
    public static Vec operator +(Vec left, Vec right) => new(left.X + right.X, left.Y + right.Y);
    public static Vec operator *(Vec left, int scalar) => new(left.X * scalar, left.Y * scalar);
}
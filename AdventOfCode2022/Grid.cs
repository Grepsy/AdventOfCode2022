using System.Collections;
using System.Text;

namespace AdventOfCode2022;

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
        var list = from y in Enumerable.Range(0, grid.LengthY - 1) from x in Enumerable.Range(0, grid.LengthX - 1) select grid[x, y];
        return list.GetEnumerator();
    }

    public IEnumerable<Grid<T>> Neighbors() => new[] { this[-1, 0], this[0, -1], this[1, 0], this[0, 1] }.Where(x => x.InRange);

    public override string ToString() {
        var sb = new StringBuilder();
        foreach (var row in Items) {
            sb.AppendJoin('\0', row).AppendLine();
        }
        return sb.ToString();
    }

    public static Grid<T> FromSparse(IEnumerable<(Vec pos, T val)> items, T emptyValue = default) {
        var maxX = items.Max(i => i.pos.X);
        var maxY = items.Max(i => i.pos.Y);
        var lookup = items.DistinctBy(x => x.pos).ToDictionary(x => x.pos, x => x.val);

        var array = new T[maxY + 1][];
        for (var y = 0; y < array.Length; y++) {
            array[y] = new T[maxX + 1];
            for (var x = 0; x < array[0].Length; x++) {
                array[y][x] = lookup.TryGetValue((x, y), out var val) ? val : emptyValue;
            }
        }

        return new Grid<T>(array, (0, 0));
    }
}
using static MoreLinq.Extensions.SliceExtension;
using static MoreLinq.Extensions.TakeUntilExtension;
using static MoreLinq.Extensions.TransposeExtension;

namespace AdventOfCode2022;

public static class Day08 {
    public static object Run1() { // 1779
        var grid = File.ReadAllText("day8.txt").Split("\n").Select(x => x.ToCharArray().Select(x => x - '0').ToArray()).ToArray();
        var grid2 = grid.Transpose().Select(x => x.ToArray()).ToArray();

        var r =
            from y in Enumerable.Range(1, grid.Length - 2)
            from x in Enumerable.Range(1, grid.Length - 2)
            let h = grid[y][x]
            let lv = grid[y].Slice(0, x).All(v => v < h)
            let rv = grid[y].Skip(x + 1).All(v => v < h)
            let tv = grid2[x].Slice(0, y).All(v => v < h)
            let bv = grid2[x].Skip(y + 1).All(v => v < h)
            select lv || rv || tv || bv;

        return r.Count(x => x) + grid.Length * 2 + grid[0].Length * 2 - 4;
    }

    public static object Run2() { // 172224
        var grid = File.ReadAllText("day8.txt").Split("\n").Select(x => x.ToCharArray().Select(x => x - '0').ToArray()).ToArray();
        var grid2 = grid.Transpose().Select(x => x.ToArray()).ToArray();

        return (
            from y in Enumerable.Range(0, grid.Length - 1)
            from x in Enumerable.Range(0, grid.Length - 1)
            let h = grid[y][x]
            let lv = grid[y].Slice(0, x).Reverse().TakeUntil(v => v >= h).Count()
            let rv = grid[y].Skip(x + 1).TakeUntil(v => v >= h).Count()
            let tv = grid2[x].Slice(0, y).Reverse().TakeUntil(v => v >= h).Count()
            let bv = grid2[x].Skip(y + 1).TakeUntil(v => v >= h).Count()
            select lv * rv * tv * bv).Max();
    }
}
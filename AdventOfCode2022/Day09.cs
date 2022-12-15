namespace AdventOfCode2022;

public static class Day09 {
    public static object Run1() => Run2(size: 2); //6236

    public static object Run2(int size = 10) { // 2449
        var steps =
            from line in File.ReadLines("day9.txt").Select(x => x.Split(' '))
            let direction = line[0] switch {
                "U" => new Vec(0, 1),
                "D" => new Vec(0, -1),
                "L" => new Vec(-1, 0),
                "R" => new Vec(1, 0)
            }
            from step in Enumerable.Repeat(direction, int.Parse(line[1]))
            select step;

        var knots = Enumerable.Repeat(Vec.Zero, size).ToArray();
        var visits = new HashSet<Vec>();

        foreach (var step in steps) {
            knots[0] += step;
            for (var i = 1; i < knots.Length; i++) {
                knots[i] += (knots[i - 1] - knots[i]) switch {
                    (0, var y) { Length: 2 } => new Vec(0, Math.Sign(y)),
                    (var x, 0) { Length: 2 } => new Vec(Math.Sign(x), 0),
                    (var x, var y) { Length: > 2 } => new Vec(Math.Sign(x), Math.Sign(y)),
                    _ => Vec.Zero
                };
            }
            visits.Add(knots[size - 1]);
        }

        return visits.Count;
    }
}
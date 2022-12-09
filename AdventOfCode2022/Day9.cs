namespace AdventOfCode2022;

public static class Day9 {
    public static object Run1() { //
        var steps =
            from line in File.ReadLines("day9.txt").Select(x => x.Split(' '))
            let direction = line[0] switch {
                "U" => new V(0, 1),
                "D" => new V(0, -1),
                "L" => new V(-1, 0),
                "R" => new V(1, 0)
            }
            from step in Enumerable.Repeat(direction, int.Parse(line[1]))
            select step;

        V head = V.Zero, tail = V.Zero;
        var visits = new HashSet<V>();

        foreach (var step in steps) {
            head += step;
            tail += (head - tail) switch {
                (0, var y) when Math.Abs(y) == 2 => new V(0, Math.Sign(y)),
                (var x, 0) when Math.Abs(x) == 2 => new V(Math.Sign(x), 0),
                (var x, var y) { Length: > 2 } => new V(Math.Sign(x), Math.Sign(y)),
                _ => V.Zero
            };
            visits.Add(tail);
        }

        return visits.Count;
    }

    public static object Run2() { // 
        var steps =
            from line in File.ReadLines("day9.txt").Select(x => x.Split(' '))
            let direction = line[0] switch {
                "U" => new V(0, 1),
                "D" => new V(0, -1),
                "L" => new V(-1, 0),
                "R" => new V(1, 0)
            }
            from step in Enumerable.Repeat(direction, int.Parse(line[1]))
            select step;

        var knots = Enumerable.Repeat(V.Zero, 10).ToArray();
        var visits = new HashSet<V>();

        foreach (var step in steps) {
            knots[0] += step;
            for (var i = 1; i < knots.Length; i++) {
                knots[i] += (knots[i - 1] - knots[i]) switch {
                    (0, var y) when Math.Abs(y) == 2 => new V(0, Math.Sign(y)),
                    (var x, 0) when Math.Abs(x) == 2 => new V(Math.Sign(x), 0),
                    (var x, var y) { Length: > 2 } => new V(Math.Sign(x), Math.Sign(y)),
                    _ => V.Zero
                };
            }
            visits.Add(knots[9]);
        }

        return visits.Count;
    }

    public static void Print(params V[] knots) {
        for (int y = 4; y >= 0; y--) {
            for (int x = 0; x < 6; x++) {
                var i = Array.FindIndex(knots, knot => knot.X == x && knot.Y == y);
                Console.Write(i >= 0 ? i.ToString() : ".");
            }
            Console.WriteLine();
        }
    }
}

public record V(int X, int Y) {
    public static readonly V Zero = new (0, 0);
    public int Length => Math.Abs(X) + Math.Abs(Y);
    public static V operator -(V left, V right) => new(left.X - right.X, left.Y - right.Y);
    public static V operator +(V left, V right) => new(left.X + right.X, left.Y + right.Y);
    public static V operator *(V left, int scalar) => new(left.X * scalar, left.Y * scalar);
}
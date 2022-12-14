using System.Text.Json.Serialization;

namespace AdventOfCode2022;

public readonly record struct Vec(int X, int Y) {
    public static readonly Vec Zero = new(0, 0);

    [JsonIgnore] public int Length => Math.Abs(X) + Math.Abs(Y);
    [JsonIgnore] public Vec Norm => Length == 0 ? Zero : this / Length;

    public static IEnumerable<Vec> Lerp(Vec from, Vec to) {
        var v = to - from;
        return Enumerable.Range(0, v.Length).Select(x => from + v.Norm * x);
    }

    public override string ToString() => $"({X}, {Y})";

    public static Vec Create(int[] xy) => new(xy[0], xy[1]);
    public static Vec Create(IEnumerable<int> xy) => new(xy.ElementAt(0), xy.ElementAt(1));

    public static implicit operator Vec((int x, int y) tuple) => new(tuple.x, tuple.y);
    public static Vec operator -(Vec left, Vec right) => new(left.X - right.X, left.Y - right.Y);
    public static Vec operator +(Vec left, Vec right) => new(left.X + right.X, left.Y + right.Y);
    public static Vec operator *(Vec left, Vec right) => new(left.X * right.X, left.Y * right.Y);
    public static Vec operator /(Vec left, Vec right) => new(left.X / right.X, left.Y / right.Y);
    public static Vec operator *(Vec left, int scalar) => new(left.X * scalar, left.Y * scalar);
    public static Vec operator /(Vec left, int scalar) => new(left.X / scalar, left.Y / scalar);
}
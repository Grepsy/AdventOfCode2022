using System.Text.Json.Nodes;
using MoreLinq;

namespace AdventOfCode2022;

public static class Day13 {
    public static readonly PacketComparer Comparer = new();
    public static readonly string[] Markers = { "[[2]]", "[[6]]" };

    public static int Run1() => File.ReadAllLines("day13.txt")
        .Batch(3)
        .Select((l, i) => l.Select(x => JsonNode.Parse(x)).Apply(IsRightOrder) ? i + 1 : 0)
        .Sum();

    public static int Run2() => File.ReadAllLines("day13.txt")
        .Concat(Markers)
        .Where(x => !string.IsNullOrEmpty(x))
        .Select(x => JsonNode.Parse(x))
        .Order(Comparer)
        .Index()
        .Where(x => Markers.Contains(x.Value!.ToJsonString()))
        .Select(x => x.Key + 1)
        .Aggregate((x, y) => x * y);

    private static bool IsRightOrder(JsonNode? a, JsonNode? b) => Comparer.Compare(a, b) == -1;

    public class PacketComparer : IComparer<JsonNode?> {
        public int Compare(JsonNode? a, JsonNode? b) => (a, b) switch {
            (not null, null) => 1,
            (null, not null) => -1,
            (JsonValue left, JsonValue right) => ((int)left).CompareTo((int)right),
            (JsonArray left, JsonValue right) => Compare(left, new JsonArray((int)right)),
            (JsonValue left, JsonArray right) => Compare(new JsonArray((int)left), right),
            (JsonArray left, JsonArray right) => left.ZipLongest(right, Compare).FirstOrDefault(x => x != 0)
        };
    }
}
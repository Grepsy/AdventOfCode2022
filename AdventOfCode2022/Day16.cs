using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace AdventOfCode2022 {
    public record Valve(string Id, int Flow, string[] Tunnels);

    public static class Day16 {
        public static Regex matcher = new(@"([A-J]{2}).*?(\d+).*?(?:([A-J]{2})[, ]*)+");

        public static object Run1() {
            var valves =
                (from line in File.ReadAllLines("day16.s.txt")
                 let match = matcher.Matches(line)[0].Groups
                 let tunnels = match[3].Captures.Select(x => x.Value).ToArray()
                 select new Valve(Id: match[1].Value, Flow: int.Parse(match[2].Value), tunnels)).ToDictionary(x => x.Id);

            int visit(string id, int depth, int minLeft, ImmutableArray<string> open) => (minLeft, depth, valves[id]) switch {
                ( <= 0, _, _) or (_, > 15, _) => 0,
                ( > 0, var d, Valve v) when v.Flow != 0 && !open.Contains(id) => Math.Max(
                    v.Flow * (minLeft - 1) + v.Tunnels.Max(x => visit(x, d + 1, minLeft - 2, open.Add(id))),
                    v.Tunnels.Max(x => visit(x, d + 1, minLeft - 1, open))),
                ( > 0, var d, Valve v) => v.Tunnels.Max(x => visit(x, d + 1, minLeft - 1, open)),
            };

            var start = valves.First().Key;

            return visit(start, 0, minLeft: 30, ImmutableArray<string>.Empty);
        }

        public static object Run2() {
            return 0;
        }
    }
}

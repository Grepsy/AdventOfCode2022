using System.Collections.Immutable;
using System.Text.RegularExpressions;
using static MoreLinq.Extensions.ForEachExtension;
using static MoreLinq.Extensions.TakeUntilExtension;

namespace AdventOfCode2022 {
    public record Valve(string Id, int Flow, Dictionary<string, int> Tunnels) {
        public void Replace(string oldId, Dictionary<string, int> tunnels) {
            if (Tunnels.ContainsKey(oldId)) {
                foreach (var (newId, length) in tunnels) {
                    var newLength = length + Tunnels[oldId];
                    if (Tunnels.ContainsKey(newId) && Tunnels[newId] <= newLength) {
                        $"{oldId}: {Id} -> {newId} {length}, {Tunnels[newId]} KEEP".Log();
                    }
                    else {
                        Tunnels[newId] = newLength;
                        $"{oldId}: {Id} -> {newId} {length}, {Tunnels[newId]}".Log();
                    }
                }

                Tunnels.Remove(Id);
                Tunnels.Remove(oldId);
            }
        }
    }

    public enum Status { Closed, Open };

    public static class Day16 {
        public static Regex matcher = new(@"([A-Z]{2}).*?(\d+).*?(?:([A-Z]{2})[, ]*)+");

        public static object Run1() {
            var valves =
                (from line in File.ReadAllLines("day16.s.txt")
                 let match = matcher.Matches(line)[0].Groups
                 let tunnels = match[3].Captures.ToDictionary(x => x.Value, _ => 1)
                 select new Valve(Id: match[1].Value, Flow: int.Parse(match[2].Value), tunnels)).ToArray();

            var start = valves[0];
            var queue = new Queue<Valve>(valves);
            queue.Enqueue(queue.Dequeue());

            while (queue.Peek() != start) {
                var item = queue.Dequeue();
                if (item.Flow == 0) {
                    queue.ForEach(x => x.Replace(item.Id, item.Tunnels));
                }
                else {
                    queue.Enqueue(item);
                }
            }

            //queue.Dump(true);

            var valveMap = queue.ToDictionary(x => x.Id);

            int visit(string id, Status? newStatus, int depth, int minLeft, ImmutableStack<(string id, Status status)> path) {
                var valve = valveMap[id];
                var status = newStatus ?? path.FirstOrDefault(x => x.id == id).status;
                path = path.Push((id, status));

                var blocked = path.TakeUntil(x => x.status == Status.Open).Select(x => x.id).ToHashSet();
                var next = valve.Tunnels.Where(x => x.Value <= minLeft && !blocked.Contains(x.Key)).ToArray();

                if (false) {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"D: {depth} Min: {30 - minLeft} ");
                    foreach (var i in path.Reverse()) {
                        Console.ForegroundColor = i == path.First() ? ConsoleColor.Green : blocked.Contains(i.id) ? ConsoleColor.DarkRed : ConsoleColor.White;
                        Console.Write($"{i.id}({i.status.ToString()[0]}) > ");
                    }

                    foreach (var i in valve.Tunnels) {
                        Console.ForegroundColor = i.Key == id ? ConsoleColor.Green : blocked.Contains(i.Key) ? ConsoleColor.DarkRed : ConsoleColor.White;
                        Console.Write($"{i} | ");
                    }
                    Console.ReadLine();
                }

                return (minLeft, status) switch {
                    (0, _) => 0,
                    ( > 0, Status.Closed) when valve.Flow > 0 => Math.Max(
                        visit(id, Status.Open, depth, minLeft - 1, path) + valve.Flow * (minLeft - 1),
                        next.Select(x => visit(x.Key, null, depth + 1, minLeft - x.Value, path)).DefaultIfEmpty(0).Max()
                    ),
                    ( > 0, _) => next.Select(x => visit(x.Key, null, depth + 1, minLeft - x.Value, path)).DefaultIfEmpty(0).Max()
                };
            }

            // 2110 too high

            var stats = ImmutableStack<(string, Status)>.Empty;

            return visit(start.Id, Status.Closed, 0, minLeft: 30, stats);
        }

        public static object Run2() {
            return 0;
        }
    }
}

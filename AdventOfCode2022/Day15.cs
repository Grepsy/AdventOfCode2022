using System.Text.RegularExpressions;
using MoreLinq;
using static System.Linq.Enumerable;

namespace AdventOfCode2022 {
    public static class Day15 {
        private static (Vec sensor, Vec beacon, int dist)[] ParseInput() =>
            (from line in File.ReadAllLines("day15.txt")
                    let x = Regex.Matches(line, @"-?\d+").Select(n => int.Parse(n.Value)).ToArray()
                    let sensor = new Vec(x[0], x[1])
                    let beacon = new Vec(x[2], x[3])
                    select (sensor, beacon, dist: (beacon - sensor).Length)).ToArray();

        public static object Run1() {
            var items = ParseInput();
            var (min, max) = (items.Min(x => x.sensor.X - x.dist), items.Max(x => x.sensor.X + x.dist));

            return (from x in Range(min, max - min).AsParallel()
                    let pos = new Vec(x, 2000000)
                    select (items.Any(i => pos.Distance(i.sensor) <= i.dist) && items.All(i => i.beacon != pos)) ? 1 : 0).Sum();
        }

        public static object Run2() {
            var items = ParseInput();
            var size = 4_000_000;
            long result = 0;

            Parallel.For(0, size, (y, state) => {
                var x = (from item in items
                         let width = item.dist - Math.Abs(y - item.sensor.Y)
                         where width >= 0
                         let interval = new Interval(item.sensor.X - width, item.sensor.X + width + 1)
                         orderby interval.Start
                         select interval)
                        .Scan((a, b) => new Interval(Math.Max(a.End, b.Start), b.End))
                        .Pairwise((a, b) => (a.End, b.Start))
                        .FirstOrDefault(x => x.End != x.Start).End;

                if (x != 0) {
                    result = (long)x * size + y;
                    state.Stop();
                }
            });

            return result;
        }

        public static void Print(Vec center, int buffer, (Vec sensor, Vec beacon, int dist)[] items) {
            for (var y = center.Y - buffer; y < center.Y + buffer; y++) {
                for (var x = center.X - buffer; x < center.X + buffer; x++) {
                    var pos = new Vec(x, y);
                    var inRange = false;

                    foreach (var item in items) {
                        if (item.beacon == pos) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        }

                        inRange |= pos.Distance(item.sensor) <= item.dist;
                        if (inRange) {
                            Console.ForegroundColor = (ConsoleColor)(Array.IndexOf(items, item) % 15);
                            break;
                        }
                    }

                    Console.Write(inRange ? "#" : ".");
                }

                Console.WriteLine();
            }
        }
    }
}
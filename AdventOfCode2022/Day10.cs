using MoreLinq;

namespace AdventOfCode2022;

public static class Day10 {
    public static object Run1() {
        var ci = 1;
        var x = 1;
        var signal = 0;

        var table = new Dictionary<string, (int cycles, Action<int[]>)> {
            { "noop", (1, _ => { } )},
            { "addx", (2, args => x += args[0]) }
        };

        var program = File.ReadAllLines("day10.txt").Select(x => x.Split(' '));

        foreach (var line in program) {
            var (cycles, inst) = table[line[0]];
            while (cycles-- > 0) {
                if ((ci + 20) % 40 == 0) {
                    signal += ci * x;
                }

                ci++;
            }

            inst(line[1..].Select(int.Parse).ToArray());
        }

        return signal;
    }

    public static object Run2() { // 2449
        var ci = 1;
        var x = 1;
        var crt = new bool[240];

        var table = new Dictionary<string, (int cycles, Action<int[]>)> {
            { "noop", (1, _ => { } )},
            { "addx", (2, args => x += args[0]) }
        };

        var program = File.ReadAllLines("day10.txt").Select(x => x.Split(' '));

        foreach (var line in program) {
            var (cycles, inst) = table[line[0]];
            while (cycles-- > 0) {
                var lp = (ci - 1) % 40;
                crt[ci - 1] = x >= lp - 1 && x <= lp + 1;
                ci++;
            }

            inst(line[1..].Select(int.Parse).ToArray());
        }

        crt.Batch(40).Select(l => new string(l.Select(x => x ? '#' : '.').ToArray())).ForEach(x => Console.WriteLine(x));

        return 0;
    }
}
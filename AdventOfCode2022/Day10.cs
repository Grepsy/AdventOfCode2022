using MoreLinq;

namespace AdventOfCode2022;

public static class Day10 {
    public static object Run1() {
        int x = 1, signal = 0;
        var noop = () => { };

        var program =
            (from line in File.ReadAllLines("day10.txt").Select(x => x.Split(' '))
             from cycle in line switch {
                 ["addx", var y] => new[] { noop, () => x += int.Parse(y) },
                 _ => new[] { noop }
             }
             select cycle).ToArray();

        for (var ci = 1; ci < program.Length; ci++) {
            if ((ci + 20) % 40 == 0) {
                signal += ci * x;
            }

            program[ci - 1]();
        }

        return signal;
    }

    public static object Run2() {
        var x = 1;
        var crt = new bool[240];
        var noop = () => { };

        var program =
            (from line in File.ReadAllLines("day10.txt").Select(x => x.Split(' '))
             from cycle in line switch {
                 ["addx", var y] => new[] { noop, () => x += int.Parse(y) },
                 _ => new[] { noop }
             }
             select cycle).ToArray();

        for (var ci = 0; ci < program.Length; ci++) {
            var lp = ci % 40;
            crt[ci] = x >= lp - 1 && x <= lp + 1;
            program[ci]();
        }

        return string.Join("\n", crt.Batch(40).Select(l => new string(l.Select(x => x ? '#' : '.').ToArray())));
    }
}
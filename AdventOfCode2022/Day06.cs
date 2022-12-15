using MoreLinq;

namespace AdventOfCode2022;

public static class Day06 {
    public static object Run1(int size = 4) { // 1848
        var signal = File.ReadAllText("day6.txt");
        var marker = signal.WindowLeft(size).First(x => x.Distinct().Exactly(size));

        return signal.IndexOf(new string(marker.ToArray())) + size;
    }

    public static object Run2() => Run1(size: 14); // 2308
}
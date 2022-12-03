using MoreLinq;

namespace AdventOfCode2022;

public static class Day3 {
    public static object Run1() => // 7581
        (from backpack in File.ReadLines("day3.txt")
         let halfLength = backpack.Length / 2
         let left = backpack[..halfLength]
         let right = backpack[^halfLength..]
         let item = (int)left.Intersect(right).First()
         let priority = item - (item <= 'Z' ? 'A' - 27 : 'a' - 1)
         select priority).Sum();

    public static object Run2() => // 2525
        (from party in File.ReadLines("day3.txt").Batch(3, Enumerable.ToArray)
         let item = (int)party[0].Intersect(party[1]).Intersect(party[2]).First()
         let priority = item - (item <= 'Z' ? 'A' - 27 : 'a' - 1)
         select priority).Sum();
}
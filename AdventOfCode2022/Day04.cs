namespace AdventOfCode2022;

public static class Day04 {
    public static object Run1() => // 528
        (from line in File.ReadLines("day4.txt")
         let sections = line.Split(',').Select(s => s.Split("-").Select(int.Parse).ToList()).ToList()
         let a = sections[0]
         let b = sections[1]
         let c1 = a[0] >= b[0] && a[1] <= b[1]
         let c2 = b[0] >= a[0] && b[1] <= a[1]
         select c1 || c2 ? 1 : 0).Sum();

    public static object Run2() => // 881
        (from line in File.ReadLines("day4.txt")
         let sections = line.Split(',').Select(s => s.Split("-").Select(int.Parse).ToList()).ToList()
         let a = sections[0]
         let b = sections[1]
         let o = (a[0] >= b[0] && a[0] <= b[1]) ||
                 (a[1] >= b[0] && a[1] <= b[1]) ||
                 (b[0] >= a[0] && b[0] <= a[1]) ||
                 (b[1] >= a[0] && b[1] <= a[1])
         select o ? 1 : 0).Sum();
}
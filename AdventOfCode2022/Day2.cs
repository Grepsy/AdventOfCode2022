namespace AdventOfCode2022;

public static class Day2 {
    public static object Run1() { // 13484
        return
            (from line in File.ReadLines("day2.txt")
             let round = (p2: (char)(line[2] - 23), p1: line[0])
             let pointsA = round.p2 - 64
             let pointsB = round switch {
                 ('A', 'B') => 0,
                 ('A', 'C') => 6,
                 ('B', 'A') => 6,
                 ('B', 'C') => 0,
                 ('C', 'A') => 0,
                 ('C', 'B') => 6,
                 _ => 3
             }
             select pointsA + pointsB).Sum();
    }

    public static object Run2() { // 13433
        return
            (from line in File.ReadLines("day2.txt")
             let round = (r: line[2], p1: line[0])
             let points = round switch {
                 ('X', 'A') => 3 + 0,
                 ('X', 'B') => 1 + 0,
                 ('X', 'C') => 2 + 0,
                 ('Y', 'A') => 1 + 3,
                 ('Y', 'B') => 2 + 3,
                 ('Y', 'C') => 3 + 3,
                 ('Z', 'A') => 2 + 6,
                 ('Z', 'B') => 3 + 6,
                 ('Z', 'C') => 1 + 6
             }
             select points).Sum();
    }
}
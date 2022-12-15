using static MoreLinq.Extensions.WindowExtension;
namespace AdventOfCode2022;

public static class Day14 {
    public static Vec[] Moves = { (0, 1), (-1, 1), (1, 1), (0, 0) };
    public static Vec Spawn = (500, 0);

    private static HashSet<Vec> ParseRocks() => File.ReadAllLines("day14.txt")
        .Select(l => l.Split("->").Select(x => Vec.Create(x.Split(",").Select(int.Parse))))
        .SelectMany(w => w.Window(2).SelectMany(x => x.Apply(Vec.Lerp)).Append(w.Last()))
        .ToHashSet();

    public static int Run1() {
        var blocks = ParseRocks();
        int abyss = 1000, rockCount = blocks.Count;
        var isFree = Not<Vec>(blocks.Contains);
        Vec grain, move;
        
        do {
            grain = Spawn;

            do grain += move = Moves.Select(x => x + grain).First(isFree);
            while (move != Vec.Zero && grain.Y != abyss);
            blocks.Add(grain);
        } while (grain.Y != abyss);

        return blocks.Count - rockCount - 1;
    }

    public static int Run2() {
        var blocks = ParseRocks();
        int floor = blocks.Max(v => v.Y), rockCount = blocks.Count;
        Vec grain, move;

        do {
            grain = Spawn;
            do grain += move = Moves.First(x => !blocks.Contains(grain + x));
            while (move != Vec.Zero && grain.Y <= floor);
            blocks.Add(grain);
        } while (grain != Spawn);

        return blocks.Count - rockCount;
    }
}

//var min = blocks.Min(p => p.X);
//Console.SetCursorPosition(0, 0);
//Grid<char>.FromSparse(blocks.Append(grain).Select(x => (x - (min, 0), x == grain ? 'O' : '#')), ' ').Log();
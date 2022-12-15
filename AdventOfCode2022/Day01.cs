namespace AdventOfCode2022;

public static class Day01 {
    public static int Run() =>
        (from elf in File.ReadAllText("day1.txt").Split("\n\n")
         let foods = elf.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
         let totalCalories = foods.Sum()
         select totalCalories).OrderDescending().Take(3).Sum();
}
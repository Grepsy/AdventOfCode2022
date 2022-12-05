using System.Text.RegularExpressions;
using MoreLinq;
using static System.Linq.Enumerable;

namespace AdventOfCode2022;

public static class Day5 {
    public static object Run1() { // TLNGFGMFN
        var sections = File.ReadAllText("day5.txt").Split("\n\n").ToList();
        var stacksText = sections[0].Split("\n").ToList();
        var stacks =
            (from col in Range(0, stacksText[0].Length / 4 + 1)
             let crates = Range(0, stacksText.Count - 1)
                 .Select(row => stacksText[row][col * 4 + 1])
                 .Where(char.IsAsciiLetter).Reverse()
             select new Stack<char>(crates)).ToList();

        var operations =
            from line in sections[1].Split("\n", StringSplitOptions.RemoveEmptyEntries)
            let matches = Regex.Matches(line, @"\d+").Select(x => int.Parse(x.Value)).ToList()
            select (count: matches[0], src: matches[1] - 1, dst: matches[2] - 1);

        foreach (var (count, src, dst) in operations) {
            Range(0, count).ForEach(_ => stacks[dst].Push(stacks[src].Pop()));
        }

        return new string(stacks.Select(Enumerable.First).ToArray());
    }

    public static object Run2() { // FGLQJCMBD
        var sections = File.ReadAllText("day5.txt").Split("\n\n").ToList();
        var stacksText = sections[0].Split("\n").ToList();
        var stacks =
            (from col in Range(0, stacksText[0].Length / 4 + 1)
             let crates = Range(0, stacksText.Count - 1)
                 .Select(row => stacksText[row][col * 4 + 1])
                 .Where(char.IsAsciiLetter).Reverse()
             select new Stack<char>(crates)).ToList();

        var operations =
            from line in sections[1].Split("\n", StringSplitOptions.RemoveEmptyEntries)
            let matches = Regex.Matches(line, @"\d+").Select(x => int.Parse(x.Value)).ToList()
            select (count: matches[0], src: matches[1] - 1, dst: matches[2] - 1);

        foreach (var (count, src, dst) in operations) {
            Range(0, count).Select(_ => stacks[src].Pop()).Reverse().ForEach(stacks[dst].Push);
        }

        return new string(stacks.Select(Enumerable.First).ToArray());
    }
}
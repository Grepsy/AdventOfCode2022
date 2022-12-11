namespace AdventOfCode2022;

public static class Day11 {
    public static Monkey[] GetMonkeys() => new[] {
        new Monkey(x => x * 7, 2, 7, 1, 62, 92, 50, 63, 62, 93, 73, 50),
        new Monkey(x => x + 3, 7, 2, 4, 51, 97, 74, 84, 99),
        new Monkey(x => x + 4, 13, 5, 4, 98, 86, 62, 76, 51, 81, 95),
        new Monkey(x => x + 5, 19, 6, 0, 53, 95, 50, 85, 83, 72),
        new Monkey(x => x * 5, 11, 5, 3, 59, 60, 63, 71),
        new Monkey(x => x * x, 5, 6, 3, 92, 65),
        new Monkey(x => x + 8, 3, 0, 7, 78),
        new Monkey(x => x + 1, 17, 2, 1, 84, 93, 54)
    };

    public static object Run1() {
        var monkeys = GetMonkeys();

        for (var round = 1; round <= 20; round++) {
            foreach (var monkey in monkeys) {
                while (monkey.Items.TryDequeue(out var oldValue)) {
                    monkey.Inspections++;
                    var newValue = monkey.Operation(oldValue) / 3;
                    var next = newValue % monkey.Divisor == 0 ? monkey.OutA : monkey.OutB;
                    monkeys[next].Items.Enqueue(newValue);
                }
            }
        }

        var insp = monkeys.Select(x => x.Inspections).OrderDescending().ToArray();

        return insp[0] * insp[1];
    }

    public static object Run2() {
        var monkeys = GetMonkeys();
        var comDiv = monkeys.Select(x => x.Divisor).Aggregate((a, b) => a * b);

        for (var round = 1; round <= 10000; round++) {
            foreach (var monkey in monkeys) {
                while (monkey.Items.TryDequeue(out var oldValue)) {
                    monkey.Inspections++;
                    var newValue = monkey.Operation(oldValue) % comDiv;
                    var next = newValue % monkey.Divisor == 0 ? monkey.OutA : monkey.OutB;
                    monkeys[next].Items.Enqueue(newValue);
                }
            }
        }

        var insp = monkeys.Select(x => x.Inspections).OrderDescending().ToArray();

        return insp[0] * insp[1];
    }
}

public record Monkey(Func<long, long> Operation, long Divisor, long OutA, long OutB, params long[] items) {
    public Queue<long> Items { get; } = new(items);
    public long Inspections { get; set; }
}
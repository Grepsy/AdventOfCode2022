namespace AdventOfCode2022 {
    public readonly record struct Interval {
        public readonly int Start { get; init; }
        public readonly int End { get; init; }
        public readonly int Length => End - Start;

        public Interval(int start, int end) {
            Start = start;
            End = Math.Max(start, end);
        }
    }
}
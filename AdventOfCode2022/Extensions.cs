namespace AdventOfCode2022;

public static class Extensions {
    public static T Dump<T>(this T obj) {
        Console.WriteLine(obj);
        return obj;
    }
}
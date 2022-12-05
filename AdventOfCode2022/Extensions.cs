using System.Text.Json;

namespace AdventOfCode2022;

public static class Extensions {
    public static T Dump<T>(this T obj) {
        var text = JsonSerializer.Serialize(obj, typeof(T), new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(text);
        return obj;
    }

    public static IEnumerable<T> Dump<T>(this IEnumerable<T> obj, int size = 32) {
        var text = JsonSerializer.Serialize(obj.Take(size), typeof(IEnumerable<T>), new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(text);
        return obj;
    }
}
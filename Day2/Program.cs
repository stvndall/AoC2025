// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;
using Advent.Utils;

internal class Program
{
    private static ConcurrentDictionary<long, bool> cache = new();

    public static void Main(string[] args)
    {
        var line = FileReader.ReadAllText("./files/set1.txt").Replace("\n", ",").Split(",")
            .Where(x => !string.IsNullOrEmpty(x));

        var ranges = line.Select(x =>
        {
            var parts = x.Split("-");
            return (long.Parse(parts[0]), long.Parse(parts[1]));
        });
        List<long> duplicates = ranges.SelectMany(FindDuplicates).ToList();
        Console.WriteLine($"Found {duplicates.Count} duplicates:");
        foreach (var number in duplicates)
        {
            Console.WriteLine(number);
        }

        Console.WriteLine($"Result: {duplicates.Sum()}");
    }

    private static IEnumerable<long> FindDuplicates((long, long) range)
    {
        for (long i = range.Item1; i <= range.Item2; i++)
        {
            var containsDups = cache.GetOrAdd(i, i =>
            {
                HashSet<string> parts = [];
                var s = i.ToString();
                for (int len = 1; len < s.Length; len++)
                {
                    if (s.Length % len != 0) continue;
                    parts.Clear();
                    for (int start = 0; start < s.Length; start += len)
                    {
                        parts.Add(s.Substring(start, len));
                    }

                    if (parts.Count == 1)
                    {
                        return true;
                    }
                    //69553832684
                }
                /*
                for (int len = 1; len <= max; len++)
                {
                    for (int start = 0; start <= s.Length - 2 * len; start++)
                    {
                        var part1 = s.Substring(start, len);
                        var part2 = s.Substring(start + len, len);
                        if (part1 == part2)
                        {
                            return true;
                        }
                    }
                }
                */

                return false;
            });
            if (containsDups)
            {
                yield return i;
            }
        }
    }
}
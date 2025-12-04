// See https://aka.ms/new-console-template for more information

using Advent.Utils;

var lines = FileReader.ReadLines("./files/set1.txt");
var sum = lines.Select(GetNumber).Aggregate((acc, next) => acc + next);
Console.WriteLine($"Part 1: {sum}");

ulong GetNumber(string line)
{
    var span = line.AsSpan();
    Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
    for (int i = 0; i < span.Length; i++)
    {
        var digit = span[i] - '0';
        if (!map.ContainsKey(digit))
        {
            map[digit] = new List<int>();
        }

        map[digit].Add(i);
    }

    var keys = map.Keys.OrderDescending().ToArray();
    var index = 0;
    var highest = (keys[0], map[keys[0]].First());
    while (highest.Item2 >= (span.Length - 12))
    {
        index++;
        highest = (keys[index], map[keys[index]].First());
    }

    //order by index descending
    // var numbers = new []{highest}.Concat( keys.SelectMany(x => map[x].Where(i => i > highest.Item2).Select(i => (x, i))).Take(11))
    //     .OrderByDescending(x => x.Item2).ToArray();
    var numbers = GetLargestAvailableNumber(map, span.Length,12).OrderByDescending(x => x.Item2).ToArray();
    // create the number from the digits multiplied by their position
    ulong result = 0;
    ulong position = 1;
    foreach (var (digit, _) in numbers)
    {
        result += position * (ulong)digit;
        position *= 10;
    }
        

    return result;
}

IEnumerable<(int, int)> GetLargestAvailableNumber(Dictionary<int, List<int>> map, int originalLen ,int getTotal)
{
    //get the largest available number from the map generated from getTotal amount of numbers
    var index = 0;
    for(int i = 0; i < getTotal; i++)
    {
        var (digit, position) = GetLargestInRange(map, index, originalLen - getTotal + i);
        yield return (digit, position);
        index = position + 1;
    }
}

(int, int) GetLargestInRange(Dictionary<int, List<int>> map, int startIndex, int endIndex)
{
    for (int i = 9; i >= 0; i--)
    {
        if (!map.TryGetValue(i, out var list)) 
            continue;
        var filtered = list.Where(x => x >= startIndex && x <= endIndex).ToArray();
        if (filtered.Any())
        {
            return (i, filtered.First());
        }
    }
    Console.WriteLine("Something went wrong");
    throw new System.Exception();
}



using System.Diagnostics.Contracts;
using Advent.Utils;

var lines = FileReader.ReadLines("Files/set1.txt").Select(x => x.Trim());
var ranges = new List<(long Start, long End)>();
var check = new List<long>();
var addToRanges = true;


foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line))
    {
        addToRanges = false;
        continue;
    }

    if (addToRanges)
    {
        var parts = line.Split('-');
        ranges.Add((long.Parse(parts[0]), long.Parse(parts[1])));
    }
    else
    {
        check.Add(long.Parse(line));
    }
}
var orderedRanges = ranges.OrderBy(x => x.Start).ToList();
var mergedRanges = MergeRanges(orderedRanges).ToList();
int count = 0;
foreach (var l in check)
{
    if(mergedRanges.Any(x => l >= x.Start && l <= x.End))
    {
        Console.WriteLine(l);
        count++;
    }
}

var total = 0L;
foreach (var mergedRange in mergedRanges)
{
    total += mergedRange.End - mergedRange.Start +1;
}
Console.WriteLine("Part 1 Result: " + count);
Console.WriteLine("Part 2 Result: " + total);

IEnumerable<(long Start, long End)> MergeRanges(List<(long Start, long End)> valueTuples)
{
    (long Start, long End) latest = valueTuples[0];
    foreach (var range in valueTuples)
    {
        if (range.Start <= latest.End + 1)
        {
            latest = (latest.Start, Math.Max(latest.End, range.End));
        }
        else
        {
            yield return latest;
            latest = range;
        }
    }

    yield return latest;
}
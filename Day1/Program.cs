// See https://aka.ms/new-console-template for more information

using Advent.Utils;

var lines = FileReader.ReadLines("./files/set1.txt");
var numbers = lines.Select(GetNumberForLine);
int midpoint = 50;
int range = midpoint * 2;
int start = 50;
int counter = 0;

_ = numbers.Aggregate(start, (current, number) =>
{
    int times = number / range;
    counter += Math.Abs(times);
    int next = (current + (number % range));
    switch (next)
    {
        case > 99:
            next %= range;
            if (next != 0)
            {
                counter++;
            }
            break;
        case < 0:
            next = (next % range) + range;
            if (current != 0)
            {
                counter++;
            }

            break;
    }

    if (next == 0)
    {
        counter++;
    }

    return next;
});

Console.WriteLine($"Result: {counter}");


static int GetNumberForLine(string line)
{
    var direction = line[0];
    var number = (int.Parse(line[1..]));
    return direction == 'L' ? -number : number;
}
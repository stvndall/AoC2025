namespace Advent.Utils;

public class FileReader
{
    public static string[] ReadLines(string filePath)
    {
        return System.IO.File.ReadAllLines(filePath);
    }
    
    public static string ReadAllText(string filePath)
    {
        return System.IO.File.ReadAllText(filePath);
    }

    public static T[][] MakeGrid<T>(string path, Func<char, T> func)
    {
        var lines = ReadLines(path);
        return lines.Select(line => line.Select(func).ToArray()).ToArray();
    }
}
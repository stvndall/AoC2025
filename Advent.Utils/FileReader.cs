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
}
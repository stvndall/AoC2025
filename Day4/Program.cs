// See https://aka.ms/new-console-template for more information

using Advent.Utils;

var grid = FileReader.MakeGrid("./files/set1.txt", c => c == '@');

int rows = grid.Length;
int cols = grid[0].Length;
int previousHit = -1;
int hit = 0;
while (hit != previousHit)
{
    previousHit = hit;
    for (int r = 0; r < rows; r++)
    {
        for (int c = 0; c < cols; c++)
        {
            if (grid[r][c] == false) continue;
            var neighbors = GetNeighbors(grid, r, c).ToArray();
            if (neighbors.Count(x => x) < 4)
            {
                grid[r][c] = false;
                ++hit;
            }
        }
    }
}

Console.WriteLine(hit);
return;

IEnumerable<bool> GetNeighbors(bool[][] grid, int row, int column)
{
    for (int rowIndex = row - 1; rowIndex <= row + 1; rowIndex++)
    {
        if (rowIndex >= 0 && rowIndex < grid.Length)
        {
            for (int columnIndex = column - 1; columnIndex <= column + 1; columnIndex++)
            {
                if (rowIndex == row && columnIndex == column) continue;
                if (columnIndex >= 0 && columnIndex < grid[0].Length)
                {
                    yield return grid[rowIndex][columnIndex];
                }
            }
        }
    }
}
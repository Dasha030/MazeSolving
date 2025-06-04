using System.IO;

public static class MazeLoader
{
    public static Maze LoadFromFile(string path)
    {
        var lines = File.ReadAllLines(path);
        int rows = lines.Length;
        int cols = lines[0].Length;
        var maze = new Maze(rows, cols);

        for (int i = 0; i < rows; i++)
        {
            if (lines[i].Length != cols)
                throw new Exception("Невірна структура лабіринту");

            for (int j = 0; j < cols; j++)
            {
                maze.Grid[i, j] = lines[i][j] switch
                {
                    'S' => CellType.Start,
                    'E' => CellType.End,
                    '#' => CellType.Wall,
                    '.' => CellType.Empty,
                    _ => throw new Exception("Невідомий символ у файлі")
                };
            }
        }

        return maze;
    }
}

using System;

public static class MazeGenerator
{
    private static Random rand = new();

    public static Maze Generate(int rows, int cols, double complexity = 0.3)
    {
        var maze = new Maze(rows, cols);
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                maze.SetCell(i, j, rand.NextDouble() < complexity ? CellType.Wall : CellType.Empty);

        maze.SetCell(0, 0, CellType.Start);
        maze.SetCell(rows - 1, cols - 1, CellType.End);
        return maze;
    }
}

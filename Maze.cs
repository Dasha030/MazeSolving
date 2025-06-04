public class Maze
{
    public CellType[,] Grid { get; set; }
    public int Rows => Grid.GetLength(0);
    public int Columns => Grid.GetLength(1);

    public Maze(int rows, int columns)
    {
        Grid = new CellType[rows, columns];
    }

    public CellType GetCell(int row, int col) => Grid[row, col];
    public void SetCell(int row, int col, CellType type) => Grid[row, col] = type;
}

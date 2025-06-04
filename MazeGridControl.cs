using System.Drawing;
using System.Windows.Forms;

public class MazeGridControl : Control
{
    public Maze Maze { get; set; }
    public int CellSize { get; set; } = 25;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (Maze == null) return;

        for (int i = 0; i < Maze.Rows; i++)
        {
            for (int j = 0; j < Maze.Columns; j++)
            {
                var rect = new Rectangle(j * CellSize, i * CellSize, CellSize, CellSize);
                var brush = Maze.GetCell(i, j) switch
                {
                    CellType.Wall => Brushes.Black,
                    CellType.Start => Brushes.Green,
                    CellType.End => Brushes.Red,
                    CellType.Path => Brushes.Blue,
                    CellType.Visited => Brushes.LightGray,
                    _ => Brushes.White
                };

                e.Graphics.FillRectangle(brush, rect);
                e.Graphics.DrawRectangle(Pens.Gray, rect);
            }
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace MazePathfinder
{
    public class MazeControl : Panel
    {
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public int CellSize { get; private set; } = 20;
        public int[,] MazeGrid { get; private set; }

        public Point? StartPoint { get; set; }
        public Point? EndPoint { get; set; }
        public List<Point> Path { get; set; } = new();

        private Random random = new();

        public MazeControl()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            Resize += (s, e) => Invalidate();
        }

        public void GenerateMaze(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            MazeGrid = new int[rows, cols];

            // Генерація: 0 - прохід, 1 - стіна
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    MazeGrid[i, j] = random.NextDouble() < 0.3 ? 1 : 0;

            StartPoint = null;
            EndPoint = null;
            Path.Clear();
            Invalidate();
        }

        public void LoadMaze(int[,] loadedMaze)
        {
            Rows = loadedMaze.GetLength(0);
            Cols = loadedMaze.GetLength(1);
            MazeGrid = loadedMaze;
            StartPoint = null;
            EndPoint = null;
            Path.Clear();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (MazeGrid == null) return;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Rectangle rect = new(j * CellSize, i * CellSize, CellSize, CellSize);
                    Color color = MazeGrid[i, j] == 1 ? Color.Black : Color.White;
                    using Brush brush = new SolidBrush(color);
                    e.Graphics.FillRectangle(brush, rect);
                    e.Graphics.DrawRectangle(Pens.Gray, rect);
                }
            }

            // Start and End
            if (StartPoint.HasValue)
                e.Graphics.FillRectangle(Brushes.Green, StartPoint.Value.X * CellSize, StartPoint.Value.Y * CellSize, CellSize, CellSize);
            if (EndPoint.HasValue)
                e.Graphics.FillRectangle(Brushes.Red, EndPoint.Value.X * CellSize, EndPoint.Value.Y * CellSize, CellSize, CellSize);

            // Path
            foreach (var point in Path)
                e.Graphics.FillRectangle(Brushes.Blue, point.X * CellSize + 4, point.Y * CellSize + 4, CellSize - 8, CellSize - 8);
        }

        public void SetStart(Point point)
        {
            StartPoint = point;
            Invalidate();
        }

        public void SetEnd(Point point)
        {
            EndPoint = point;
            Invalidate();
        }

        public void SetPath(List<Point> path)
        {
            Path = path;
            Invalidate();
        }

        public bool IsWall(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Cols || y >= Rows) return true;
            return MazeGrid[y, x] == 1;
        }

        public bool IsInside(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Cols && y < Rows;
        }
    }
}

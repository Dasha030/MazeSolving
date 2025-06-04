using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MazeSolver
{
    public partial class Form1 : Form
    {
        private MazeControl mazeControl;

        public Form1()
        {
            InitializeComponent();
            InitializeMazeUI();
        }

        private void InitializeMazeUI()
        {
            this.Text = "Maze Pathfinding";
            this.Size = new Size(1000, 700);

            mazeControl = new MazeControl
            {
                Location = new Point(20, 20),
                Size = new Size(600, 600),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(mazeControl);

            var btnGenerate = new Button
            {
                Text = "Generate Maze",
                Location = new Point(650, 50),
                Size = new Size(200, 40)
            };
            btnGenerate.Click += (s, e) => mazeControl.GenerateRandomMaze(20, 20);
            this.Controls.Add(btnGenerate);

            var btnLoad = new Button
            {
                Text = "Load from File",
                Location = new Point(650, 100),
                Size = new Size(200, 40)
            };
            btnLoad.Click += (s, e) =>
            {
                var ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    mazeControl.LoadMazeFromFile(ofd.FileName);
                }
            };
            this.Controls.Add(btnLoad);

            var btnStart = new Button
            {
                Text = "Start Search",
                Location = new Point(650, 200),
                Size = new Size(200, 40)
            };
            btnStart.Click += (s, e) => mazeControl.StartPathfinding();
            this.Controls.Add(btnStart);
        }
    }
}

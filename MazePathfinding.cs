using System;
using System.Collections.Generic;

namespace MazePathfinding
{
    public class MazeGenerator
    {
        private readonly Random _rand = new Random();

        public int[,] GenerateMaze(int width, int height)
        {
            if (width % 2 == 0) width++;
            if (height % 2 == 0) height++;

            int[,] maze = new int[height, width];

            // Initialize maze with walls
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    maze[y, x] = 1;

            CarvePassagesFrom(1, 1, maze);
            return maze;
        }

        private void CarvePassagesFrom(int cx, int cy, int[,] maze)
        {
            int[] directions = { 0, 1, 2, 3 };
            Shuffle(directions);

            foreach (int direction in directions)
            {
                int nx = cx, ny = cy;
                switch (direction)
                {
                    case 0: nx = cx + 2; break; // Right
                    case 1: ny = cy + 2; break; // Down
                    case 2: nx = cx - 2; break; // Left
                    case 3: ny = cy - 2; break; // Up
                }

                if (ny > 0 && ny < maze.GetLength(0) && nx > 0 && nx < maze.GetLength(1) && maze[ny, nx] == 1)
                {
                    maze[cy + (ny - cy) / 2, cx + (nx - cx) / 2] = 0;
                    maze[ny, nx] = 0;
                    CarvePassagesFrom(nx, ny, maze);
                }
            }
        }

        private void Shuffle(int[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = _rand.Next(i + 1);
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }
    }
}

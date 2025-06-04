using System;

namespace LabyrinthSolver
{
    public class Pathfinder
    {
        private Maze maze;

        public Pathfinder(Maze maze)
        {
            this.maze = maze;
        }

        public void FindPathDijkstra()
        {
            Console.WriteLine("Алгоритм Дейкстри ще не реалізований (заглушка).");
        }

        public void FindPathAStar(bool useManhattan)
        {
            Console.WriteLine($"Алгоритм A* з {(useManhattan ? "манхеттенською" : "евклідовою")} евристикою ще не реалізований (заглушка).");
        }
    }
}
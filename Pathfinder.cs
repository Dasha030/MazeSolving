using System;
using System.Collections.Generic;

public enum SearchMethod
{
    Dijkstra,
    AStarManhattan,
    AStarEuclidean
}

public static class Pathfinder
{
    public static List<(int, int)> FindPath(Maze maze, SearchMethod method)
    {
        // Стандартна реалізація A*/Dijkstra
        // Використовує Manhattan/Euclidean/0 евристику
        var start = Find(maze, CellType.Start);
        var end = Find(maze, CellType.End);
        var cameFrom = new Dictionary<(int, int), (int, int)?>();
        var cost = new Dictionary<(int, int), double>();

        var pq = new PriorityQueue<(int, int), double>();
        pq.Enqueue(start, 0);
        cameFrom[start] = null;
        cost[start] = 0;

        while (pq.Count > 0)
        {
            var current = pq.Dequeue();
            if (current == end)
                break;

            foreach (var next in GetNeighbors(current, maze))
            {
                double newCost = cost[current] + 1;
                if (!cost.ContainsKey(next) || newCost < cost[next])
                {
                    cost[next] = newCost;
                    double priority = newCost + Heuristic(next, end, method);
                    pq.Enqueue(next, priority);
                    cameFrom[next] = current;
                }
            }
        }

        return ReconstructPath(cameFrom, end);
    }

    private static double Heuristic((int, int) a, (int, int) b, SearchMethod method) =>
        method switch
        {
            SearchMethod.Dijkstra => 0,
            SearchMethod.AStarManhattan => Math.Abs(a.Item1 - b.Item1) + Math.Abs(a.Item2 - b.Item2),
            SearchMethod.AStarEuclidean => Math.Sqrt(Math.Pow(a.Item1 - b.Item1, 2) + Math.Pow(a.Item2 - b.Item2, 2)),
            _ => 0
        };

    private static List<(int, int)> GetNeighbors((int, int) pos, Maze maze)
    {
        var result = new List<(int, int)>();
        var (r, c) = pos;
        var dirs = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

        foreach (var (dr, dc) in dirs)
        {
            int nr = r + dr, nc = c + dc;
            if (nr >= 0 && nr < maze.Rows && nc >= 0 && nc < maze.Columns &&
                maze.GetCell(nr, nc) != CellType.Wall)
            {
                result.Add((nr, nc));
            }
        }
        return result;
    }

    private static (int, int) Find(Maze maze, CellType type)
    {
        for (int i = 0; i < maze.Rows; i++)
            for (int j = 0; j < maze.Columns; j++)
                if (maze.GetCell(i, j) == type)
                    return (i, j);
        throw new Exception($"Клітинка типу {type} не знайдена.");
    }

    private static List<(int, int)> ReconstructPath(Dictionary<(int, int), (int, int)?> cameFrom, (int, int) end)
    {
        var path = new List<(int, int)>();
        var current = end;

        while (cameFrom.ContainsKey(current) && cameFrom[current] != null)
        {
            path.Add(current);
            current = cameFrom[current].Value;
        }

        path.Reverse();
        return path;
    }
}

public class AStarPathfinderEuclidean : PathfinderBase
{
    public AStarPathfinderEuclidean(int[,] maze) : base(maze) { }

    private double Heuristic((int x, int y) a, (int x, int y) b) => Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));

    public override bool FindPath((int x, int y) start, (int x, int y) end)
    {
        var openSet = new PriorityQueue<(int x, int y), double>();
        var cameFrom = new (int x, int y)?[width, height];
        var gScore = new Dictionary<(int x, int y), double> { [start] = 0 };

        openSet.Enqueue(start, Heuristic(start, end));

        var directions = new[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

        while (openSet.Count > 0)
        {
            var current = openSet.Dequeue();

            if (current == end)
            {
                Path = new();
                var cur = end;
                while (cur != start)
                {
                    Path.Add(cur);
                    cur = cameFrom[cur.x, cur.y].Value;
                }
                Path.Add(start);
                Path.Reverse();
                return true;
            }

            foreach (var dir in directions)
            {
                int nx = current.x + dir.Item1;
                int ny = current.y + dir.Item2;
                var neighbor = (nx, ny);

                if (nx >= 0 && ny >= 0 && nx < width && ny < height && maze[nx, ny] == 0)
                {
                    double tentativeG = gScore[current] + 1;
                    if (!gScore.ContainsKey(neighbor) || tentativeG < gScore[neighbor])
                    {
                        gScore[neighbor] = tentativeG;
                        cameFrom[nx, ny] = current;
                        double fScore = tentativeG + Heuristic(neighbor, end);
                        openSet.Enqueue(neighbor, fScore);
                    }
                }
            }
        }

        return false;
    }
}
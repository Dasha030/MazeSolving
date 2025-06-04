public class DijkstraPathfinder : PathfinderBase
{
    public DijkstraPathfinder(int[,] maze) : base(maze) { }

    public override bool FindPath((int x, int y) start, (int x, int y) end)
    {
        var visited = new bool[width, height];
        var dist = new int[width, height];
        var prev = new (int x, int y)?[width, height];
        var pq = new PriorityQueue<(int x, int y), int>();

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                dist[i, j] = int.MaxValue;

        dist[start.x, start.y] = 0;
        pq.Enqueue(start, 0);

        var directions = new[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

        while (pq.Count > 0)
        {
            var current = pq.Dequeue();
            if (visited[current.x, current.y]) continue;
            visited[current.x, current.y] = true;

            if (current == end)
            {
                Path = new List<(int x, int y)>();
                var cur = end;
                while (cur != start)
                {
                    Path.Add(cur);
                    cur = prev[cur.x, cur.y].Value;
                }
                Path.Add(start);
                Path.Reverse();
                return true;
            }

            foreach (var dir in directions)
            {
                int nx = current.x + dir.Item1;
                int ny = current.y + dir.Item2;

                if (nx >= 0 && ny >= 0 && nx < width && ny < height && maze[nx, ny] == 0 && !visited[nx, ny])
                {
                    int newDist = dist[current.x, current.y] + 1;
                    if (newDist < dist[nx, ny])
                    {
                        dist[nx, ny] = newDist;
                        prev[nx, ny] = current;
                        pq.Enqueue((nx, ny), newDist);
                    }
                }
            }
        }
        return false;
    }
}
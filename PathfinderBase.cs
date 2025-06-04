public abstract class PathfinderBase
{
    protected readonly int[,] maze;
    protected readonly int width;
    protected readonly int height;

    public List<(int x, int y)> Path { get; protected set; } = new();

    protected PathfinderBase(int[,] maze)
    {
        this.maze = maze;
        this.width = maze.GetLength(0);
        this.height = maze.GetLength(1);
    }

    public abstract bool FindPath((int x, int y) start, (int x, int y) end);
}
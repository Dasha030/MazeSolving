public class Cell
{
    public int X { get; set; }
    public int Y { get; set; }
    public CellType Type { get; set; }
    public bool IsVisited { get; set; }
}

public enum CellType
{
    Empty,
    Wall,
    Start,
    End,
    Path,
    Visited
}

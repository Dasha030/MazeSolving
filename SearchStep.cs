public class SearchStep
{
    public (int row, int col) Position { get; set; }
    public double Cost { get; set; }
    public double Heuristic { get; set; }
}

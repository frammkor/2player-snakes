namespace Lab_4___Snake
{
    public class Cell
    {
        public int X { get; }
        public int Y { get; }
        public CellStatus Status { get; set; }

        public Cell(int x, int y, CellStatus initialStatus = CellStatus.IsFree)
        {
            this.X = x;
            this.Y = y;
            this.Status = initialStatus;
        }
    }
}
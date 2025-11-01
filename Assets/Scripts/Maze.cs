public class Maze
{
    public MazeCell[,] Cells;

    public Maze(int sizeX, int sizeZ)
    {
        Cells = new MazeCell[sizeX, sizeZ];
    }
}

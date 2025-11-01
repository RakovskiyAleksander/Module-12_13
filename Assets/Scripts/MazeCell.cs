public class MazeCell
{
    public int PositionX;
    public int PositionZ;

    public bool WallFirst = true;
    public bool WallSecond = true;
    public bool WallThird = true;
    public bool WallFourth = true;

    public bool Visited = false;
    public bool IsFinishCell = false;
    public int StackCount;
    public int AmountWall = 4;
}


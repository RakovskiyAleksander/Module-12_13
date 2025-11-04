public class MazeCell
{
    public int PositionX;
    public int PositionZ;

    public bool IsthmusUp = false;
    public bool IsthmusRigth = false;
    public bool IsthmusDown = false;
    public bool IsthmusLeft = false;

    public bool Visited = false;
    public bool IsFinishCell = false;
    public int StackCount;
    public int AmountWall = 4;
}


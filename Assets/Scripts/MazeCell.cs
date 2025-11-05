public class MazeCell
{
    public int PositionX;
    public int PositionZ;

    public bool SegmentUp = false;
    public bool SegmentRigth = false;
    public bool SegmentDown = false;
    public bool SegmentLeft = false;

    public bool Visited = false;
    public bool IsFinishCell = false;
    public int StackCount;
    public int AmountSegments = 0;
}


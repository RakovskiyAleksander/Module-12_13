public class MazeCell
{
    public int PositionX;
    public int PositionZ;

    public bool SegmentUpIsActive = false;
    public bool SegmentRightIsActive = false;
    public bool SegmentDownIsActive = false;
    public bool SegmentLeftIsActive = false;

    public bool IsStartCell = false;
    public bool Visited = false;
    public int StackCount;
    public int AmountSideSegments = 0;
}


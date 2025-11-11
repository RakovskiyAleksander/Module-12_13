using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    private int _startPointX;
    private int _startPointZ;

    public Maze GenerateMaze(int sizeX, int sizeY, int startPointX, int startPointZ)
    {
        _startPointX = startPointX;
        _startPointZ = startPointZ;

        Maze maze = new Maze(sizeX, sizeY);

        for (int x = 0; x < maze.Cells.GetLength(0); x++)
        {
            for (int z = 0; z < maze.Cells.GetLength(1); z++)
            {
                maze.Cells[x, z] = new MazeCell { PositionX = x, PositionZ = z };
            }
        }

        AddSegmentWithBacktracker(maze.Cells);

        return maze;
    }

    private void AddSegmentWithBacktracker(MazeCell[,] maze)
    {
        MazeCell startCell = maze[_startPointX, _startPointZ];
        startCell.IsStartCell = true;
        MazeCell current = startCell;
        current.Visited = true;

        Stack<MazeCell> stack = new Stack<MazeCell>();
        do
        {
            List<MazeCell> unvisitedNeighbours = new List<MazeCell>();
            int x = current.PositionX;
            int z = current.PositionZ;

            if (x > 0 && !maze[x - 1, z].Visited) unvisitedNeighbours.Add(maze[x - 1, z]);
            if (z > 0 && !maze[x, z - 1].Visited) unvisitedNeighbours.Add(maze[x, z - 1]);
            if (x < maze.GetLength(0) - 1 && !maze[x + 1, z].Visited) unvisitedNeighbours.Add(maze[x + 1, z]);
            if (z < maze.GetLength(1) - 1 && !maze[x, z + 1].Visited) unvisitedNeighbours.Add(maze[x, z + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeCell chosen = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
                AddSegment(current, chosen);

                chosen.Visited = true;
                stack.Push(chosen);
                current = chosen;
                current.StackCount = stack.Count;
            }
            else
            {
                if (current.StackCount > startCell.StackCount) startCell = current;
                current = stack.Pop();
            }

        } while (stack.Count > 0);
    }

    private void AddSegment(MazeCell current, MazeCell chosen)
    {
        if (current.PositionX == chosen.PositionX)
        {
            if (current.PositionZ < chosen.PositionZ)
            {
                current.SegmentRightIsActive = true;
                chosen.SegmentLeftIsActive = true;
            }
            else
            {
                current.SegmentLeftIsActive = true;
                chosen.SegmentRightIsActive = true;
            }
        }
        else
        {
            if (current.PositionX < chosen.PositionX)
            {
                current.SegmentDownIsActive = true;
                chosen.SegmentUpIsActive = true;
            }
            else
            {
                current.SegmentUpIsActive = true;
                chosen.SegmentDownIsActive = true;
            }
        }

        current.AmountSideSegments++;
        chosen.AmountSideSegments++;
    }
}

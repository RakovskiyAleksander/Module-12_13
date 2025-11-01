using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    public MazeCell[,] GenerateMaze(int sizeX, int sizeY)
    {
        Maze maze = new Maze(sizeX, sizeY);

        for (int x = 0; x < maze.Cells.GetLength(0); x++)
        {
            for (int z = 0; z < maze.Cells.GetLength(1); z++)
            {
                maze.Cells[x, z] = new MazeCell { PositionX = x, PositionZ = z };
            }
        }

        RemoveWallsWithBacktracker(maze.Cells);

        return maze.Cells;
    }

    private void RemoveWallsWithBacktracker(MazeCell[,] maze)
    {
        MazeCell current = maze[0, 0];
        MazeCell finishCell = maze[0, 0];
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
                RemoveWall(current, chosen);

                chosen.Visited = true;
                stack.Push(chosen);
                current = chosen;
                current.StackCount = stack.Count;
            }
            else
            {
                if (current.StackCount > finishCell.StackCount) finishCell = current;
                current = stack.Pop();

            }

        } while (stack.Count > 0);
        finishCell.IsFinishCell = true;

    }

    private void RemoveWall(MazeCell current, MazeCell chosen)
    {
        if (current.PositionX == chosen.PositionX)
        {
            if (current.PositionZ < chosen.PositionZ)
            {
                current.WallFirst = false;
                chosen.WallThird = false;
            }
            else
            {
                current.WallThird = false;
                chosen.WallFirst = false;
            }
        }
        else
        {
            if (current.PositionX < chosen.PositionX)
            {
                current.WallSecond = false;
                chosen.WallFourth = false;
            }
            else
            {
                current.WallFourth = false;
                chosen.WallSecond = false;
            }
        }
        current.AmountWall--;
        chosen.AmountWall--;
    }
}

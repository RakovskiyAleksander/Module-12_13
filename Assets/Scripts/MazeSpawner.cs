using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] float _mazeCellWidthX;
    [SerializeField] float _mazecellWidthZ;
    [SerializeField] CompositePlatform _platformPrefab;
    [SerializeField] GameObject _coinPrefab;

    public void SpawnMaze(int sizeX, int sizeY)
    {
        MazeGenerator generator = new MazeGenerator();
        Maze maze = generator.GenerateMaze(sizeX, sizeY);

        for (int x = 0; x < maze.Cells.GetLength(0); x++)
        {
            for (int z = 0; z < maze.Cells.GetLength(1); z++)
            {
                CompositePlatform compositePlatform = Instantiate(_platformPrefab, new Vector3(x * _mazeCellWidthX, 0, z * _mazecellWidthZ), Quaternion.identity);
                compositePlatform.SetActiveSegmentUp(maze.Cells[x, z].SegmentUpIsActive);
                compositePlatform.SetActiveSegmentRight(maze.Cells[x, z].SegmentRightIsActive);
                compositePlatform.SetActiveSegmentDown(maze.Cells[x, z].SegmentDownIsActive);
                compositePlatform.SetActiveSegmentLeft(maze.Cells[x, z].SegmentLeftIsActive);
                if (maze.Cells[x, z].AmountSideSegments == 1) 
                {
                    Instantiate(_coinPrefab, new Vector3(x * _mazeCellWidthX, 1, z * _mazecellWidthZ), Quaternion.identity);
                }
            }
        }
    }
}

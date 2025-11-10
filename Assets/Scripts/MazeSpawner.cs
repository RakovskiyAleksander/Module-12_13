using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] float _intervalBetweenCellsX;
    [SerializeField] float _intervalBetweenCellsZ;
    [SerializeField] CompositePlatform _platformPrefab;
    [SerializeField] GameObject _coinPrefab;
    [SerializeField] float _coinHeightGeneration;


    public void SpawnMaze(int sizeX, int sizeY, int startPointX, int startPointZ, out int coinAmount, out Vector3 PlayerStartPosition)
    {
        MazeGenerator generator = new MazeGenerator();
        Maze maze = generator.GenerateMaze(sizeX, sizeY, startPointX, startPointZ);

        int coinAmountTemp = 0;
        PlayerStartPosition = new Vector3(startPointX * _intervalBetweenCellsX, 0, startPointZ * _intervalBetweenCellsZ);

        for (int x = 0; x < maze.Cells.GetLength(0); x++)
        {
            for (int z = 0; z < maze.Cells.GetLength(1); z++)
            {
                CompositePlatform compositePlatform = Instantiate(_platformPrefab, new Vector3(x * _intervalBetweenCellsX, 0, z * _intervalBetweenCellsZ), Quaternion.identity);
                compositePlatform.SetActiveSegmentUp(maze.Cells[x, z].SegmentUpIsActive);
                compositePlatform.SetActiveSegmentRight(maze.Cells[x, z].SegmentRightIsActive);
                compositePlatform.SetActiveSegmentDown(maze.Cells[x, z].SegmentDownIsActive);
                compositePlatform.SetActiveSegmentLeft(maze.Cells[x, z].SegmentLeftIsActive);

                if (maze.Cells[x, z].AmountSideSegments == 1 && maze.Cells[x, z].IsStartCell == false)
                {
                    Instantiate(_coinPrefab, new Vector3(x * _intervalBetweenCellsX, _coinHeightGeneration, z * _intervalBetweenCellsZ), Quaternion.identity);
                    coinAmountTemp++;
                }
            }
        }

        coinAmount = coinAmountTemp;
    }
}

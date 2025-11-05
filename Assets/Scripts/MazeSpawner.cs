using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] float _mazeCellWidthX = 6f;
    [SerializeField] float _mazecellWidthZ = 6f;
    [SerializeField] CompositePlatform _platformPrefab;

    public void SpawnMaze(int sizeX, int sizeY)
    {
        MazeGenerator generator = new MazeGenerator();
        Maze maze = generator.GenerateMaze(sizeX, sizeY);

        for (int x = 0; x < maze.Cells.GetLength(0); x++)
        {
            for (int z = 0; z < maze.Cells.GetLength(1); z++)
            {
                CompositePlatform compositePlatform = Instantiate(_platformPrefab, new Vector3(x * _mazeCellWidthX, 0, z * _mazecellWidthZ), Quaternion.identity);
                
            }
        }
    }
}

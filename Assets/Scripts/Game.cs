using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int MazeSizeX = 3;
    [SerializeField] private int MazeSizeY = 3;
    [SerializeField] private MazeSpawner _mazeSpawner;

    private void Awake()
    {
        _mazeSpawner.SpawnMaze(MazeSizeX, MazeSizeY);
    }
}

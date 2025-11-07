using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Maze Setup")]
    [SerializeField] private int _mazeSizeX;
    [SerializeField] private int _mazeSizeZ;
    [SerializeField] private int _startPointX;
    [SerializeField] private int _startPointZ;
    [SerializeField] private MazeSpawner _mazeSpawner;

    [Space(10)]
    [Header("Player settings")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Vector3 _playerStartPosition;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _playerRotationSpeed;

    private int _mazeSizeXDefault = 2;
    private int _mazeSizeZDefault = 2;

    private int _startPointXDefault = 1;
    private int _startPointZDefault = 1;

    private int MazeSizeX
    {
        get
        {
            if (_mazeSizeX >= _mazeSizeXDefault) return _mazeSizeX;
            return _mazeSizeXDefault;
        }
    }

    private int MazeSizeZ
    {
        get
        {
            if (_mazeSizeZ >= _mazeSizeZDefault) return _mazeSizeZ;
            return _mazeSizeZDefault;
        }
    }

    private int StartPointX
    {
        get
        {
            if (_startPointX < _startPointXDefault || _startPointX > MazeSizeX) return _startPointXDefault;
            return _startPointX;
        }
    }

    private int StartPointZ
    {
        get
        {
            if (_startPointZ < _startPointZDefault || _startPointZ > MazeSizeZ) return _startPointZDefault;
            return _startPointZ;
        }
    }

    private void Awake()
    {
        int coinAmount;
        _mazeSpawner.SpawnMaze(MazeSizeX, MazeSizeZ, StartPointX - 1, StartPointZ - 1, out coinAmount);
    }

    public GameObject AddGameObjectToScene(GameObject gameObject, Vector3 startPoint)
    {
        GameObject newGameObject = new GameObject();
        newGameObject = Instantiate(gameObject, startPoint, Quaternion.identity);
        return newGameObject;
    }
}

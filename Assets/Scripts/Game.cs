using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [Header("Maze Setup")]
    [SerializeField] private int _mazeSizeX;
    [SerializeField] private int _mazeSizeZ;
    [SerializeField] private int _startCellX;
    [SerializeField] private int _startCellZ;
    [SerializeField] private MazeSpawner _mazeSpawner;

    [Space(10)]
    [Header("Player settings")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private float _playerStartPositionY;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _playerRotationSpeed;
    [SerializeField] private float _playerJumpForce;
    [SerializeField] private float _minDistanceGroundJump;
    [SerializeField] private float _deadlyHeight;
    [SerializeField] LayerMask _groundLayer;

    [Space(10)]
    [Header("Camera settings")]
    [SerializeField] private CameraFollow _camera;
    [SerializeField] private float _cameraSpeedFollow;
    [SerializeField] private float _cameraSpeedFollowRotation;

    [Space(10)]
    [Header("Timer settings")]
    [SerializeField] float _timeForLose;

    private GameObject _player;
    private PlayerBehaviour _playerBehaviour;

    private int _mazeSizeXDefault = 2;
    private int _mazeSizeZDefault = 2;

    private int _startPointXDefault = 1;
    private int _startPointZDefault = 1;

    private CoinCollector _coinCollector;
    private CountdownTimer _timerForLose;

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
            if (_startCellX < _startPointXDefault || _startCellX > MazeSizeX) return _startPointXDefault;
            return _startCellX;
        }
    }

    private int StartPointZ
    {
        get
        {
            if (_startCellZ < _startPointZDefault || _startCellZ > MazeSizeZ) return _startPointZDefault;
            return _startCellZ;
        }
    }

    private void Awake()
    {
        int coinAmountInLevel;
        Vector3 playerStartPosition;
        _mazeSpawner.SpawnMaze(MazeSizeX, MazeSizeZ, StartPointX - 1, StartPointZ - 1, out coinAmountInLevel, out playerStartPosition);

        _coinCollector = new CoinCollector(coinAmountInLevel);

        playerStartPosition = new Vector3(playerStartPosition.x, playerStartPosition.y + _playerStartPositionY, playerStartPosition.z);
        _player = AddGameObjectToScene(_playerPrefab, playerStartPosition);
        _playerBehaviour = _player.GetComponent<PlayerBehaviour>();
        _playerBehaviour.Initialize(_playerSpeed, _playerRotationSpeed, _playerJumpForce, _minDistanceGroundJump, _deadlyHeight, _groundLayer, _coinCollector);

        _camera.Initialize(_player, _cameraSpeedFollowRotation, _cameraSpeedFollow);

        _timerForLose = new CountdownTimer(_timeForLose);
    }

    private void Update()
    {
        _timerForLose.Update();
    }

    void OnGUI()
    {

        if (_timerForLose.IsTicking)
        {
            GUI.Box(new Rect(40, 40, 250, 25), "Время закончится через " + _timerForLose.TimeLeft + " сек!");
            GUI.Box(new Rect(40, 80, 250, 25), "Осталось собрать " + _coinCollector.CoinsLeft + " монет.");
        }

        if (_timerForLose.TimeIsOver || _playerBehaviour.IsAlive == false)
        {
            GamePause();
            GameLose();
        }

        if (_coinCollector.IsFull)
        {
            GamePause();
            GameWin();
        }
    }

    private void GamePause()
    {
        _playerBehaviour.Immobilize();
        _timerForLose.Pause();
    }

    private void GameLose()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "!!! ТЫ ПРОИГРАЛ !!!"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void GameWin()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "!!! ТЫ ВЫИГРАЛ !!!"))
        {
            SceneManager.LoadScene(0);
        }
    }

    public GameObject AddGameObjectToScene(GameObject gameObject, Vector3 startPoint)
    {
        GameObject newGameObject = new GameObject();
        newGameObject = Instantiate(gameObject, startPoint, Quaternion.identity);
        return newGameObject;
    }
}

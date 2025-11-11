using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Player _playerComponent;
    private Vector3 _playerStartPosition;

    private int _mazeSizeXDefault = 2;
    private int _mazeSizeZDefault = 2;

    private int _startPointXDefault = 1;
    private int _startPointZDefault = 1;

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
        _mazeSpawner.SpawnMaze(MazeSizeX, MazeSizeZ, StartPointX - 1, StartPointZ - 1, out coinAmount, out _playerStartPosition);
        _playerStartPosition = new Vector3(_playerStartPosition.x, _playerStartPosition.y + _playerStartPositionY, _playerStartPosition.z);
        _player = AddGameObjectToScene(_playerPrefab, _playerStartPosition);
        _playerComponent = _player.GetComponent<Player>();
        _playerComponent.Initialize(_playerSpeed, _playerRotationSpeed, _playerJumpForce, _minDistanceGroundJump, _deadlyHeight, _groundLayer);

        _camera.Initialize(_player, _cameraSpeedFollowRotation, _cameraSpeedFollow);

        _timerForLose = new CountdownTimer(_timeForLose);
    }

    private void Update()
    {
        _timerForLose.Update();
    }

    void OnGUI()
    {

        if (_timerForLose.IsTicking) GUI.Box(new Rect(40, 40, 250, 25), "Îñòàëîñü " + _timerForLose.TimeLeft + " ñåê!");

        if (_timerForLose.TimeIsOver || _playerComponent.IsAlive == false)
        {
            GamePause();
            GameLose();
        }
    }

    private void GamePause()
    {
        _playerComponent.Immobilize();
        _timerForLose.Pause();
    }

    private void GameLose()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "!!! ÒÛ ÏÐÎÈÃÐÀË !!!"))
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

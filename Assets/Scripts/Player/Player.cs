using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 DirectionMovement { get; private set; }

    private float _playerSpeed;
    private float _playerRotationSpeed;
    private float _playerJumpForce;
    private float _minDistaceGroundJump;
    private float _deadlyHeight;
    private LayerMask _groundLayer;

    private Mover _playerMover;
    private Collider _playerCollider;

    private CoinCollector _coinCollector;

    private GroundChecker _groundChecker;

    private float _vInput;
    private float _hInput;

    private bool _isJump;
    private bool _isActive;

    public bool IsAlive { get; private set; }

    public void Initialize(MovementSettings movementSettings, Wallet wallet)
    {
        _isActive = true;
        IsAlive = true;

        _playerSpeed = movementSettings.Speed;
        _playerRotationSpeed = movementSettings.RotationSpeed;
        _playerJumpForce = movementSettings.JumpForse;
        _deadlyHeight = movementSettings.DeadlyHeight;
        _minDistaceGroundJump = movementSettings.MinDistaceGroundJump;
        _groundLayer = movementSettings.GroundLayer;

        _playerMover = new Mover(GetComponent<Rigidbody>(), _playerSpeed, _playerJumpForce);
        _playerCollider = GetComponent<Collider>();

        if (TryGetComponent<CoinCollector>(out _coinCollector))
        {
            _coinCollector.Initialize(wallet);
        }

        _groundChecker = new GroundChecker(_playerCollider, _minDistaceGroundJump, _groundLayer);

        DirectionMovement = Vector3.forward;
    }

    public void Immobilize()
    {
        _isActive = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Update()
    {
        _vInput = Input.GetAxisRaw("Vertical");
        _hInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && _groundChecker.CheckGround())
        {
            _isJump = true;
        }

        if (transform.position.y < _deadlyHeight)
        {
            IsAlive = false;
        }
    }

    private void FixedUpdate()
    {
        if (_isActive)
        {
            DirectionMovement = new VectorRotate().RotateAroundAxiseY(_hInput * _playerRotationSpeed, DirectionMovement);

            if (_vInput != 0 && _groundChecker.CheckGround())
            {
                _playerMover.Move(_vInput, DirectionMovement);
            }

            if (_isJump && _groundChecker.CheckGround())
            {
                _playerMover.Jump();
                _isJump = false;
            }
        }
    }
}

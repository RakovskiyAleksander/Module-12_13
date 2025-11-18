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
    private Wallet _wallet;

    private Mover _playerMover;
    private Collider _playerCollider;

    private CoinCollector _coinCollector;

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
        _wallet = wallet;

        _playerMover = new Mover(GetComponent<Rigidbody>(), _playerSpeed, _playerRotationSpeed, _playerJumpForce);
        _playerCollider = GetComponent<Collider>();

        if (TryGetComponent<CoinCollector>(out _coinCollector))
        {
            _coinCollector.Initialize();
        }

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

        if (Input.GetKeyDown(KeyCode.Space))
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
            if (_hInput != 0)
            {
                DirectionMovement = _playerMover.RotateDirectionMovement(_hInput, DirectionMovement);
            }

            if (_vInput != 0 && IsGrounded())
            {
                _playerMover.Move(_vInput, DirectionMovement);
            }

            if (_isJump && IsGrounded())
            {
                _playerMover.Jump();
                _isJump = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Coin>(out Coin coinBehaviour))
        {
            _wallet.AddCoin();
            coinBehaviour.Collect();
        }
    }

    private bool IsGrounded()
    {
        Vector3 playerColliderBottom = new Vector3(_playerCollider.bounds.center.x, _playerCollider.bounds.min.y, _playerCollider.bounds.center.z);
        bool grounded = Physics.CheckSphere(playerColliderBottom, _minDistaceGroundJump, _groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }
}

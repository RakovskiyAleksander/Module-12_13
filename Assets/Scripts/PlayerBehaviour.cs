using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Vector3 DirectionMovement { get; private set; }

    private float _playerSpeed;
    private float _playerRotationSpeed;
    private float _playerJumpForce;
    private float _minDistaceGroundJump;
    private float _deadlyHeight;
    private LayerMask _groundLayer;
    private CoinCollector _coinCollector;

    private Mover _playerMover;
    private Collider _playerCollider;

    private float _vInput;
    private float _hInput;

    private bool _isJump;
    private bool _isActive;

    public bool IsAlive { get; private set; }

    public void Initialize(float speed, float rotationSpeed, float jumpForse, float minDistaceGroundJump, float deadlyHeight, LayerMask groundLayer, CoinCollector coinCollector)
    {
        _isActive = true;
        IsAlive = true;

        _playerSpeed = speed;
        _playerRotationSpeed = rotationSpeed;
        _playerJumpForce = jumpForse;
        _deadlyHeight = deadlyHeight;
        _minDistaceGroundJump = minDistaceGroundJump;
        _groundLayer = groundLayer;
        _coinCollector = coinCollector;

        _playerMover = new Mover(GetComponent<Rigidbody>(), _playerSpeed, _playerRotationSpeed, _playerJumpForce);
        _playerCollider = GetComponent<Collider>();

        DirectionMovement = Vector3.forward;
    }

    public void Update()
    {
        _vInput = Input.GetAxisRaw("Vertical") * _playerSpeed;
        _hInput = Input.GetAxisRaw("Horizontal") * _playerRotationSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJump = true;
        }

        if (transform.position.y < _deadlyHeight)
        {
            IsAlive = false;
        }
    }

    public void FixedUpdate()
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
        if (other.gameObject.TryGetComponent<CoinBehaviour>(out CoinBehaviour coinBehaviour))
        {
            _coinCollector.AddCoin();
            coinBehaviour.MakeInactive();
        }
    }

    private bool IsGrounded()
    {
        Vector3 playerColliderBottom = new Vector3(_playerCollider.bounds.center.x, _playerCollider.bounds.min.y, _playerCollider.bounds.center.z);
        bool grounded = Physics.CheckSphere(playerColliderBottom, _minDistaceGroundJump, _groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }

    public void Immobilize()
    {
        _isActive = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }
}

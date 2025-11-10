using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 DirectionMovement { get; private set; }

    private float _playerSpeed;
    private float _playerRotationSpeed;
    private Mover _playerMover;

    private float _vInput;
    private float _hInput;



    public void Initialize(float speed, float rotationSpeed)
    {
        _playerSpeed = speed;
        _playerRotationSpeed = rotationSpeed;
        _playerMover = new Mover(GetComponent<Rigidbody>(), _playerSpeed, _playerRotationSpeed);

        DirectionMovement = Vector3.forward;
    }

    public void Update()
    {
        _vInput = Input.GetAxisRaw("Vertical") * _playerSpeed;
        _hInput = Input.GetAxisRaw("Horizontal") * _playerRotationSpeed;
        //Debug.Log(DirectionMovement);
    }

    public void FixedUpdate()
    {
        if (_hInput != 0)
        {
            DirectionMovement = _playerMover.RotateDirectionMovement(_hInput, DirectionMovement);
        }

        if (_vInput != 0)
        {
            _playerMover.Move(_vInput, DirectionMovement);
        }

    }
}

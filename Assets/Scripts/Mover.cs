using UnityEngine;

public class Mover
{
    private float _speed;
    private float _rotationSpeed;
    private float _jumpForce;
    private Rigidbody _rigidbody;

    public Mover(Rigidbody rigidbody, float speed, float rotationSpeed, float jumpForce)
    {
        _speed = speed;
        _rotationSpeed = rotationSpeed;
        _rigidbody = rigidbody;
        _jumpForce = jumpForce;
    }

    public Vector3 RotateDirectionMovement(float input, Vector3 directionMovement)
    {
        directionMovement = Quaternion.Euler(0, _rotationSpeed * input, 0) * directionMovement;
        return directionMovement;
    }

    public void Move(float input, Vector3 directionMovement)
    {
        _rigidbody.AddForce(directionMovement * _speed * input * Time.fixedDeltaTime);
    }

    public void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}

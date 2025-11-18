using UnityEngine;

public class Mover
{
    private float _speed;
    private float _jumpForce;
    private Rigidbody _rigidbody;

    public Mover(Rigidbody rigidbody, float speed, float jumpForce)
    {
        _speed = speed;
        _rigidbody = rigidbody;
        _jumpForce = jumpForce;
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

using UnityEngine;

public class Mover
{
    private float _speed;
    private float _rotationSpeed;
    private Rigidbody _rigidbody;

    public Mover(Rigidbody rigidbody, float speed, float rotationSpeed)
    {
        _speed = speed;
        _rotationSpeed = rotationSpeed;
        _rigidbody = rigidbody;
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

}

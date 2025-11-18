using UnityEngine;

public class MovementSettings
{
    public float Speed;
    public float RotationSpeed;
    public float JumpForse;
    public float MinDistaceGroundJump;
    public float DeadlyHeight;
    public LayerMask GroundLayer;

    public MovementSettings(float speed, float rotationSpeed, float jumpForce, float minDistanceGroundJump, float deadlyHeight, LayerMask groundLayer)
    {
        Speed = speed;
        RotationSpeed = rotationSpeed;
        JumpForse = jumpForce;
        MinDistaceGroundJump = minDistanceGroundJump;
        DeadlyHeight = deadlyHeight;
        GroundLayer = groundLayer;
    }
}

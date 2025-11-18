using UnityEngine;

public class VectorRotate
{
    public Vector3 RotateAroundAxiseY(float angle, Vector3 vector) => Quaternion.Euler(0, angle, 0) * vector;
}

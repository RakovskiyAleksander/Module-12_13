using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject _target;
    private float _cameraSpeedFollowRotation;
    private float _cameraSpeedFollow;
    private Vector3 _directionMovement;

    public void Initialize(GameObject target, float cameraSpeedRotation, float cameraSpeedFollow)
    {
        _target = target;
        _cameraSpeedFollowRotation = cameraSpeedRotation;
        _cameraSpeedFollow = cameraSpeedFollow;
    }

    void LateUpdate()
    {
        _directionMovement = _target.GetComponent<Player>().DirectionMovement;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_directionMovement), _cameraSpeedFollowRotation * Time.fixedDeltaTime);
        transform.position = Vector3.Lerp(transform.position, _target.transform.position, _cameraSpeedFollow * Time.deltaTime);
    }
}

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject _target;
    private float _cameraSpeedRotation;
    private float _cameraSpeedFollow;
    private Vector3 _directionMovement;

    public void Initialize(GameObject target,float cameraSpeedRotation, float cameraSpeedFollow)
    {
        _target = target;
        _cameraSpeedRotation = cameraSpeedRotation; 
        _cameraSpeedFollow = cameraSpeedFollow;
        _directionMovement = _target.GetComponent<Player>().DirectionMovement;
    }

    void LateUpdate()
    {
        // transform.rotation = Quaternion.Lerp(Quaternion.Euler( _directionMovement), _target.transform.rotation, _cameraSpeedRotation * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(_target.GetComponent<Player>().DirectionMovement);
        Debug.Log(_target.GetComponent<Player>().DirectionMovement);
        //transform.LookAt(_target.GetComponent<Player>().DirectionMovement);
        transform.rotation = Quaternion.LookRotation(_target.GetComponent<Player>().DirectionMovement);
        transform.position = Vector3.Lerp(transform.position, _target.transform.position, _cameraSpeedFollow * Time.deltaTime);
    }
}

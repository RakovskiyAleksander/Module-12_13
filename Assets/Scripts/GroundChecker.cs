using UnityEngine;

public class GroundChecker
{
    private Collider _collider;
    private float _minDistaceGroundJump;
    private LayerMask _groundLayer;

    public GroundChecker(Collider collider, float minDistaceGroundJump, LayerMask groundLayer)
    {
        _collider = collider;
        _minDistaceGroundJump = minDistaceGroundJump;
        _groundLayer = groundLayer;
    }

    public bool CheckGround()
    {
        Vector3 playerColliderBottom = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y, _collider.bounds.center.z);
        return Physics.CheckSphere(playerColliderBottom, _minDistaceGroundJump, _groundLayer, QueryTriggerInteraction.Ignore);        
    }
}
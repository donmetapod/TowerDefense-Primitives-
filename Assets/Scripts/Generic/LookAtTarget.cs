using UnityEngine;

// Custom look at target script with customizable rotation speed and per axis offsets
public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _pivotRotationSpeed = 10;
    [SerializeField] private float _xOffset = 0;
    [SerializeField] private float _yOffset = 0;
    [SerializeField] private float _zOffset = 0;
    private void Update()
    {
        if (_target == null)
            return;

        Vector3 direction = _target.position - transform.position;
        direction.x += _xOffset;
        direction.y += _yOffset;
        direction.z += _zOffset;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
            Time.deltaTime * _pivotRotationSpeed);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void LoseTarget()
    {
        _target = null;
    }
}

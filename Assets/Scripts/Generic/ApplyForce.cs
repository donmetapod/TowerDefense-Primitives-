using UnityEngine;
using Random = UnityEngine.Random;

public class ApplyForce : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private Vector3 _forceVector;
    [SerializeField] private ForceMode _forceMode;
    [SerializeField] private float _xForce = 1;
    [SerializeField] private float _minYForce = 1;
    [SerializeField] private float _maxYForce = 1;
    [SerializeField] private float _zForce = 1;
    [SerializeField] private bool _applyForceOnAwake;
    private void Awake()
    {
        _forceVector.x = Random.Range(-_xForce, _xForce);
        _forceVector.y = Random.Range(_minYForce, _maxYForce);
        _forceVector.z = Random.Range(-_zForce, _zForce);
        if (_applyForceOnAwake)
        {
            ApplyForceVector();
        }
    }

    public void ApplyForceVector()
    {
        _rigidbody.AddForce(_forceVector, _forceMode);
    }
}

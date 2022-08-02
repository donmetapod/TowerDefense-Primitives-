using UnityEngine;

// Moves a GameObject forwards
public class MoveForward : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    private Transform _myTransform;

    private void Start()
    {
        _myTransform = transform;
    }
    void Update()
    {
        _myTransform.position += _myTransform.forward * (Time.deltaTime * _moveSpeed);
    }
}

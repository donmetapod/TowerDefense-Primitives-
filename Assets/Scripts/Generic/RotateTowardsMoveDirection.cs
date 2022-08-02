using System;
using UnityEngine;

// This scripts rotates a GameObject towards moving direction
public class RotateTowardsMoveDirection : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 50;
    private Vector3 _previousPosition;
    private float _maxRadiansDelta = 1;
    private Transform _myTransform;

    private void Start()
    {
        _myTransform = transform;
    }
    void Update()
    {
        Vector3 currentDirection = _myTransform.position - _previousPosition;
        Vector3 targetDirection = Vector3.RotateTowards(_myTransform.forward, currentDirection, _maxRadiansDelta,
            Time.deltaTime);
        _myTransform.rotation = Quaternion.RotateTowards(_myTransform.rotation, Quaternion.LookRotation(targetDirection),
            Time.deltaTime * _rotationSpeed);
        _previousPosition = _myTransform.position;
    }
}

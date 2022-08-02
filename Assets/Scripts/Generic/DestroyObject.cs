using UnityEngine;
using UnityEngine.Events;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float _destroyDelay = 0.1f;
    [SerializeField] private UnityEvent OnDestroy;
    
    public void DestroyWithDelay()
    {
        OnDestroy?.Invoke();
        Destroy(gameObject, _destroyDelay);
    }
}

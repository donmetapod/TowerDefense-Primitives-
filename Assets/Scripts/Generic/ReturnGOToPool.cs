using UnityEngine;

// Utility scripts that works in conjunction with ObjectPool script to
// return a GO to a pool stored in ObjectPool variable
public class ReturnGOToPool : MonoBehaviour
{
    public ObjectPool ObjectPool;

    private void OnDisable()
    {
        ObjectPool.ReturnGameObjectToPool(gameObject);
    }
}

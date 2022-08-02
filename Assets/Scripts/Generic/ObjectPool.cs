using System.Collections.Generic;
using UnityEngine;

// This class helps creating a pool of GameObjects from a given prefab, the object itself
// manages returning to the pool by using the ReturnGOToPool component 
public class ObjectPool
{
    public GameObject ObjectPrefab;
    private Stack<GameObject> _objectsInPool = new Stack<GameObject>();

    public GameObject GetGameObjectFromPool()
    {
        if (_objectsInPool.Count > 0)
        {
            return _objectsInPool.Pop();
        }
        return CreateNewGameObject();
    }
    
    public GameObject CreateNewGameObject()
    {

        GameObject clone = GameObject.Instantiate(ObjectPrefab);
        clone.transform.name = ObjectPrefab.name;
        if (!clone.TryGetComponent(out ReturnGOToPool component))
        {
            component = clone.AddComponent<ReturnGOToPool>();
            component.ObjectPool = this;
        }
        return clone;
    }
    
    public void ReturnGameObjectToPool(GameObject go)
    {
        _objectsInPool.Push(go);
    }

    

}

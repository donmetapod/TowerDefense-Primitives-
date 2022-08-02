using UnityEngine;

public class CreateObject : MonoBehaviour
{
    [SerializeField] private GameObject _objectToCreate;
    private Transform _spawnPoint;
    [SerializeField] private bool _useSpawnPoint;
    [Range(0,1)][SerializeField] private float _chance = 1;
    public void CreateNewObject()
    {
        if (Random.value < _chance)
        {
            if (_useSpawnPoint)
            {
                Instantiate(_objectToCreate, _spawnPoint.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_objectToCreate, transform.position, Quaternion.identity);    
            }    
        }
    }
}

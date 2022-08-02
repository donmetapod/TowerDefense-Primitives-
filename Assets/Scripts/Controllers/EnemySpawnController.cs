using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

// Spawns enemies according to data on SpawnData, can use Instantiation or pooling
public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private SpawnData _spawnData;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private UnityEvent OnWavesEnded;
    [SerializeField] private UnityEvent<int> OnNewWaveStarted;
    [SerializeField] private GameState _gameState;
    [SerializeField] private int _currentWave;
    private ObjectPool _weakEnemyPool;
    private ObjectPool _midEnemyPool;
    private ObjectPool _heavyEnemyPool;

    private void Start()
    {
        _weakEnemyPool = new ObjectPool();
        _midEnemyPool = new ObjectPool();
        _heavyEnemyPool = new ObjectPool();
        _weakEnemyPool.ObjectPrefab = _spawnData.WeakEnemyPrefab;
        _midEnemyPool.ObjectPrefab = _spawnData.MidEnemyPrefab;
        _heavyEnemyPool.ObjectPrefab = _spawnData.HeavyEnemyPrefab;
        
        StartCoroutine(CreateNewEnemyWave());    
    }
    
    
    IEnumerator CreateNewEnemyWave()
    {
        while (_currentWave < _spawnData.Waves.Length 
            && _gameState.CurrentGameState == GameState.GameStateEnum.Playing)
        {
            yield return new WaitForSeconds(_spawnData.WaitTimeBetweenWaves);
            OnNewWaveStarted?.Invoke(_currentWave+1);
            // Using Instantiate 
            // StartCoroutine(SpawnEnemies(_wavesData.Waves[_waveNumber].WeakEnemies, _weakEnemyPrefab));
            // StartCoroutine(SpawnEnemies(_wavesData.Waves[_waveNumber].MidEnemies, _midEnemyPrefab));
            // StartCoroutine(SpawnEnemies(_wavesData.Waves[_waveNumber].HeavyEnemies, _heavyEnemyPrefab));
            
            // Using pooling
            StartCoroutine(SpawnEnemiesFromPool(_spawnData.Waves[_currentWave].WeakEnemies, _weakEnemyPool));
            StartCoroutine(SpawnEnemiesFromPool(_spawnData.Waves[_currentWave].MidEnemies, _midEnemyPool));
            StartCoroutine(SpawnEnemiesFromPool(_spawnData.Waves[_currentWave].HeavyEnemies, _heavyEnemyPool));
            while (_gameState.EnemyCount > 0)
            {
                yield return null;
            }
            
            _currentWave++;
        }
        
        // Winner
        if (!_gameState.GameOver)
        {
            OnWavesEnded?.Invoke();    
        }
    }

    IEnumerator SpawnEnemiesFromPool(int enemyAmount, ObjectPool objectPool)
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            GameObject enemy = objectPool.GetGameObjectFromPool();
            enemy.transform.position = _spawnPoint.position;
            enemy.SetActive(true);
            yield return new WaitForSeconds(Random.Range(_spawnData.MinimumSpawnDelay, _spawnData.MaximumSpawnDelay));
        }
    }
    
    // IEnumerator SpawnEnemies(int enemyAmount, GameObject enemyPrefab)
    // {
    //     for (int i = 0; i < enemyAmount; i++)
    //     {
    //         Instantiate(enemyPrefab, _spawnPoint.position, Quaternion.identity);
    //         yield return new WaitForSeconds(Random.Range(_minimumSpawnDelay, _maximumSpawnDelay));
    //     }
    // }
}

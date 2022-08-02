using System;
using UnityEngine;

[CreateAssetMenu (fileName = "WavesData", menuName = "ScriptableObjects/CreateWavesData")]
public class SpawnData : ScriptableObject
{
    [Serializable]
    public struct Wave
    {
        public int WeakEnemies;
        public int MidEnemies;
        public int HeavyEnemies;
    }

    public Wave[] Waves;

    public GameObject WeakEnemyPrefab;
    public GameObject MidEnemyPrefab;
    public GameObject HeavyEnemyPrefab;
    
    public float MinimumSpawnDelay = 1;
    public float MaximumSpawnDelay = 3;
    public float WaitTimeBetweenWaves = 5;
}

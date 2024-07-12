using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private List<Wave> waves;
    private int currentWaveIndex = 0;
    private int enemiesToSpawn;
    private int enemiesSpawned;
    private int enemiesLeftToSpawn;
    
    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;
    private float _spawnTimer; 
    private ObjectPooler _pooler;

    private int activeEnemies; 

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        StartWave();
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = delayBtwSpawns;
            if (enemiesLeftToSpawn > 0)
            {
                enemiesSpawned++;
                enemiesLeftToSpawn--;
                SpawnEnemy();
            }
        }

        if (activeEnemies == 0 && enemiesLeftToSpawn == 0 && currentWaveIndex < waves.Count - 1)
        {
            currentWaveIndex++;
            StartWave();
        }
    }

    private void StartWave()
    {
        enemiesSpawned = 0;
        enemiesLeftToSpawn = waves[currentWaveIndex].defaultEnemyCount + waves[currentWaveIndex].heavyEnemyCount + waves[currentWaveIndex].stealthEnemyCount;
        enemiesToSpawn = enemiesLeftToSpawn;
    }

    private void SpawnEnemy()
    {
        Wave currentWave = waves[currentWaveIndex];
        GameObject newInstance = null;

        if (currentWave.defaultEnemyCount > 0)
        {
            newInstance = _pooler.GetInstanceFromPool("DefaultEnemy");
            currentWave.defaultEnemyCount--;
        }
        else if (currentWave.heavyEnemyCount > 0)
        {
            newInstance = _pooler.GetInstanceFromPool("HeavyEnemy");
            currentWave.heavyEnemyCount--;
        }
        else if (currentWave.stealthEnemyCount > 0)
        {
            newInstance = _pooler.GetInstanceFromPool("StealthEnemy");
            currentWave.stealthEnemyCount--;
        }

        if (newInstance != null)
        {
            newInstance.SetActive(true);
            activeEnemies++; 
            newInstance.GetComponent<Enemy>().OnEnemyDestroyed += HandleEnemyDestroyed;
        }
    }

    private void HandleEnemyDestroyed()
    {
        activeEnemies--; 
    }
}

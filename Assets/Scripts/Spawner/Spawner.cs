using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private List<Wave> waves;

    private int currentWaveIndex = 0;
    private int enemiesToSpawn;
    private int enemiesSpawned;
    private int enemiesLeftToSpawn;

    private float delayBtwSpawns = 1.5f;
    private float _spawnTimer;
    private ObjectPooler _pooler;
    private int activeEnemies;

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        InitializeWaves();
        StartWave();
    }

    private void InitializeWaves()
    {
        waves = new List<Wave>();
        
        waves.Add(new Wave {
            defaultEnemyCount = 5,
            heavyEnemyCount = 0,
            stealthEnemyCount = 0
        });
        
        waves.Add(new Wave {
            defaultEnemyCount = 7,
            heavyEnemyCount = 0,
            stealthEnemyCount = 2
        });
        
        waves.Add(new Wave {
            defaultEnemyCount = 8,
            heavyEnemyCount = 1,
            stealthEnemyCount = 3
        });
        
        waves.Add(new Wave {
            defaultEnemyCount = 10,
            heavyEnemyCount = 2,
            stealthEnemyCount = 4
        });

        waves.Add(new Wave {
            defaultEnemyCount = 10,
            heavyEnemyCount = 2,
            stealthEnemyCount = 5
        });

        waves.Add(new Wave {
            defaultEnemyCount = 12,
            heavyEnemyCount = 3,
            stealthEnemyCount = 6
        });

        waves.Add(new Wave {
            defaultEnemyCount = 15,
            heavyEnemyCount = 4,
            stealthEnemyCount = 7
        });

        waves.Add(new Wave {
            defaultEnemyCount = 18,
            heavyEnemyCount = 5,
            stealthEnemyCount = 8
        });
    }

    private void StartWave()
    {
        enemiesSpawned = 0;
        Wave currentWave = waves[currentWaveIndex];
        enemiesLeftToSpawn = currentWave.defaultEnemyCount + currentWave.heavyEnemyCount + currentWave.stealthEnemyCount;
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
}

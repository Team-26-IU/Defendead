using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    public static List<Pool> pools;
    public static Dictionary<string, Queue<GameObject>> poolDictionary;
    
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    
    [SerializeField] public GameObject defaultEnemyPrefab;  
    [SerializeField] public GameObject heavyEnemyPrefab; 
    [SerializeField] public GameObject stealthEnemyPrefab; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        pools = new List<Pool>();
        pools.Add(new Pool { tag = "DefaultEnemy", prefab = defaultEnemyPrefab, size = 85 });
        pools.Add(new Pool { tag = "HeavyEnemy", prefab = heavyEnemyPrefab, size = 24 });
        pools.Add(new Pool { tag = "StealthEnemy", prefab = stealthEnemyPrefab, size = 35 });

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject GetInstanceFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void ReturnToPool(GameObject instance)
    {
        if (instance && instance.activeSelf)
        {
            instance.SetActive(false);
            instance.GetComponent<Enemy>().ResetEnemy();
        }
    }

    public IEnumerator ReturnToPoolWithDelay(GameObject instance, float delay)
    {
        yield return new WaitForSeconds(delay);
        instance.SetActive(false);
        instance.GetComponent<Enemy>().ResetEnemy();
    }
}

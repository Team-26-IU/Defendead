using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;
    private List<GameObject> _pool;
    private GameObject _poolContainer;

    private void Awake()
    {
        _pool = new List<GameObject>();
        _poolContainer = new GameObject($"Pool - {prefab.name}");
        CreatePooler();
    }

    private void CreatePooler()
    {
        for (int i = 0; i < poolSize; i++)
        {
            _pool.Add(CreateInstance());
        }
    }

    private GameObject CreateInstance()
    {
        GameObject newInstance = Instantiate(prefab);
        newInstance.transform.SetParent(_poolContainer.transform);
        newInstance.SetActive(false);
        return newInstance;
    }

    public GameObject GetInstanceFromPool()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            // Проверяем, что объект существует и активен
            if (_pool[i] && _pool[i].activeInHierarchy)
            {
                return _pool[i];
            }
        }

        // Если не найден активный объект в пуле, создаем новый
        return CreateInstance();
    }


    public static void ReturnToPool(GameObject instance)
    {
        if (instance && instance.activeSelf)
        {
            Debug.Log($"Returning {instance.name} to pool.");
            instance.SetActive(false);
            instance.GetComponent<Enemy>().ResetEnemy();
        }
    }


    public static IEnumerator ReturnToPoolWithDelay(GameObject instance, float delay)
    {
        yield return new WaitForSeconds(delay);
        instance.SetActive(false);
        instance.GetComponent<Enemy>().ResetEnemy();  // Сбрасываем состояние врага
    }
}
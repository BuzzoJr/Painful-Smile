using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;

    public GameObject chaserPrefab;
    public GameObject shooterPrefab;
    public GameObject player;

    private Queue<GameObject> chaserPool = new Queue<GameObject>();
    private Queue<GameObject> shooterPool = new Queue<GameObject>();

    public int initialPoolSize = 10;

    void Awake()
    {
        Instance = this;
        AddObjectsToPool(chaserPool, chaserPrefab, initialPoolSize);
        AddObjectsToPool(shooterPool, shooterPrefab, initialPoolSize);
    }

    private void AddObjectsToPool(Queue<GameObject> pool, GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.gameObject.GetComponent<EnemyController>().mainTarget = player;
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetEnemy()
    {
        // 50% de chance de ser Chaser ou Shooter
        if (Random.value < 0.5f)
        {
            return GetObjectFromPool(chaserPool, chaserPrefab);
        }
        else
        {
            return GetObjectFromPool(shooterPool, shooterPrefab);
        }
    }

    private GameObject GetObjectFromPool(Queue<GameObject> pool, GameObject prefab)
    {
        if (pool.Count == 0)
        {
            AddObjectsToPool(pool, prefab, 5);
        }

        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj, bool isChaser)
    {
        obj.SetActive(false);
        if (isChaser)
        {
            chaserPool.Enqueue(obj);
        }
        else
        {
            shooterPool.Enqueue(obj);
        }
    }
}

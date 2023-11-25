using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance;
    public int initialPool = 20;
    public GameObject objectPrefab;
    private Queue<GameObject> projectilePool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < initialPool; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            projectilePool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (projectilePool.Count == 0)
        {
            AddObjects(10);
        }

        GameObject obj = projectilePool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        projectilePool.Enqueue(obj);
    }

    private void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            projectilePool.Enqueue(obj);
        }
    }
}

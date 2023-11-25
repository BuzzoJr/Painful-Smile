using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnInterval = 1f;
    public List<Transform> spawnPoints;

    private void Start()
    {
        if (PlayerPrefs.GetFloat("Enemy Spawn Rate") != 0)
            spawnInterval = PlayerPrefs.GetFloat("Enemy Spawn Rate");

        Invoke("SpawnEnemy", spawnInterval);
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Count > 0)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            GameObject enemy = EnemyPool.Instance.GetEnemy();
            enemy.transform.position = spawnPoint.position;
            enemy.GetComponent<TrailRenderer>().Clear();
            Invoke("SpawnEnemy", spawnInterval);
        }
    }
}

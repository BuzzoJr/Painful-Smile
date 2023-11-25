using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float lifespan = 5f;
    public float damage = 1f;
    public GameObject explosionPrefab;
    public List<AudioClip> clip = new List<AudioClip>();
    private AudioSource cannonSound;

    private void OnEnable()
    {
        cannonSound = GetComponent<AudioSource>();
        cannonSound.PlayOneShot(clip[Random.Range(0, clip.Count)]);

        CancelInvoke("ReturnToPool");
        Invoke("ReturnToPool", lifespan);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReturnToPool();
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().HealthController(damage);
        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().HealthController(damage);
        }
    }

    private void ReturnToPool()
    {
        CancelInvoke("ReturnToPool");
        ProjectilePool.Instance.ReturnObject(gameObject);
    }
}

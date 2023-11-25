using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public EnemyData enemyData;
    private bool isDestroyed = false;
    [Header("-----Movement-----")]
    public GameObject mainTarget;
    private NavMeshAgent agent;
    [Header("-----Attack-----")]
    public Transform projectileSpawnPoint;
    private float damage;
    private float projectileSpeed = 1f;
    private float singleShotCooldown = 0.5f;
    private float timeSinceSingleShot = 0f;
    [Header("-----Health-----")]
    public Image healthBar;
    public SpriteRenderer enemySpriteRenderer;
    private float maxHealth = 100f;
    private float healthPoints = 100f;

    public float remainingDistance;


    private void OnEnable()
    {
        isDestroyed = false;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = enemyData.MovementSpeed;
        agent.stoppingDistance = enemyData.StoppingDistance;
        damage = enemyData.Damage;
        singleShotCooldown = enemyData.ProjectileCooldown;
        projectileSpeed = enemyData.ProjectileSpeed;
        maxHealth = enemyData.Health;
        healthPoints = enemyData.Health;
        HealthController(0);
    }

    void Update()
    {
        if (mainTarget != null)
        {
            agent.SetDestination(mainTarget.transform.position);
            var vel = agent.velocity;
            vel.z = 0;

            if (vel != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward, vel);
            }
            else
            {
                Vector2 direction = mainTarget.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
            }
            remainingDistance = agent.remainingDistance;

            timeSinceSingleShot += Time.deltaTime;
            if (!enemyData.IsChaser && agent.remainingDistance <= enemyData.StoppingDistance + 0.1f && timeSinceSingleShot >= singleShotCooldown && agent.velocity.magnitude <= 0.1f)
                ShootPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(enemyData.IsChaser && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().HealthController(damage);
            HealthController(maxHealth, true);
        }
    }
    public void HealthController(float damage, bool noPoints = false)
    {
        healthPoints -= damage;
        healthBar.fillAmount = healthPoints / maxHealth;
        if (healthBar.fillAmount >= 0.7f)
        {
            enemySpriteRenderer.sprite = enemyData.SpriteList[0];
        }
        else if (healthBar.fillAmount >= 0.4f)
        {
            enemySpriteRenderer.sprite = enemyData.SpriteList[1];
        }
        else if (healthBar.fillAmount > 0f)
        {
            enemySpriteRenderer.sprite = enemyData.SpriteList[2];
        }
        else
        {
            if(!noPoints)
                mainTarget.GetComponent<PlayerController>().AddScore(enemyData.RewardPoints);
            enemySpriteRenderer.sprite = enemyData.SpriteList[3];
            if (!isDestroyed)
            {
                isDestroyed = true;
                GameObject destroyedShip = Instantiate(enemyData.EnemyPrefab);
                destroyedShip.transform.position = transform.position;
                destroyedShip.transform.rotation = transform.rotation;
            }
            EnemyPool.Instance.ReturnObject(gameObject, enemyData.IsChaser);
        }
    }

    void ShootPlayer()
    {
        if(Vector3.Distance(transform.position, mainTarget.transform.position) <= 5)
        {
            timeSinceSingleShot = 0f;
            GameObject projectile = ProjectilePool.Instance.GetObject();
            projectile.transform.position = projectileSpawnPoint.position;
            projectile.GetComponent<ProjectileController>().damage = damage;
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = transform.up * projectileSpeed;
        }
    }
}

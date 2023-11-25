using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    private float health;

    [SerializeField]
    private int rewardPoints;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float projectileCooldown;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private float stoppingDistance;

    [SerializeField]
    private bool isChaser;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private List<Sprite> spriteList;

    public float Health { get { return health; } }
    public int RewardPoints { get { return rewardPoints; } }
    public float MovementSpeed { get { return movementSpeed; } }
    public float Damage { get { return damage; } }
    public float ProjectileCooldown { get { return projectileCooldown; } }
    public float ProjectileSpeed { get { return projectileSpeed; } }
    public float StoppingDistance { get { return stoppingDistance; } }
    public bool IsChaser { get { return isChaser; } }
    public List<Sprite> SpriteList { get { return spriteList; } }
    public GameObject EnemyPrefab { get { return enemyPrefab; } }
}

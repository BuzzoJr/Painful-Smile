using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("-----Movement-----")]
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 100.0f;
    public float acceleration = 1f;
    public float deceleration = 1f;
    private float currentSpeed = 0.0f;

    [Header("-----Attack-----")]
    public float singleShotCooldown = 0.5f;
    public float specialShotCooldown = 1.5f;
    public float projectileSpeed = 10f;
    public float damage = 15f;
    public List<Transform> projectileSpawnPoint;
    private float timeSinceSingleShot = 0f;
    private float timeSinceSpecialShot = 0f;

    [Header("-----Health-----")]
    public float maxHealth = 100f;
    public float healthPoints = 100f;
    public Image healthBar;
    public SpriteRenderer playerSpriteRenderer;
    public List<Sprite> spriteList;

    [Header("-----UI-----")]
    public TextMeshProUGUI scoreUI;
    public static int score = 0;

    private AudioSource boatSound;
    private Rigidbody2D rb;
    private string currentState = "Playing";

    private void Awake()
    {
        GameManager.OnGameStateChange += GameManagerOnGameStateChange;
    }
    void OnDestroy()
    {
        GameManager.OnGameStateChange -= GameManagerOnGameStateChange;
    }
    private void GameManagerOnGameStateChange(GameManager.GameState state)
    {
        currentState = state.ToString();
        if(state == GameManager.GameState.Lose || state == GameManager.GameState.Win)
            Destroy(gameObject);
    }
    void Start()
    {
        boatSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(currentState == "Playing")
        {
            timeSinceSingleShot += Time.deltaTime;
            timeSinceSpecialShot += Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && timeSinceSingleShot >= singleShotCooldown)
            {
                ShootSingle();
                timeSinceSingleShot = 0f;
            }
            if (Input.GetMouseButtonDown(1) && timeSinceSpecialShot >= specialShotCooldown)
            {
                ShootSpecial();
                timeSinceSpecialShot = 0f;
            }

            float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, -rotation);

            float verticalInput = Input.GetAxis("Vertical");
  
            if (verticalInput != 0)
            {
                currentSpeed += verticalInput * acceleration * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, -movementSpeed, movementSpeed);
            }
            else
            {
                if (currentSpeed > 0)
                {
                    currentSpeed -= deceleration * Time.deltaTime * rb.velocity.magnitude;
                    currentSpeed = Mathf.Max(currentSpeed, 0);
                }
                else if (currentSpeed < 0)
                {
                    currentSpeed += deceleration * Time.deltaTime * rb.velocity.magnitude;
                    currentSpeed = Mathf.Min(currentSpeed, 0);
                }
            }
            boatSound.volume = Mathf.Clamp01(rb.velocity.magnitude / 20);
            rb.velocity = currentSpeed * transform.up;
        }
    }

    void ShootSingle()
    {
        GameObject projectile = ProjectilePool.Instance.GetObject();
        projectile.transform.position = projectileSpawnPoint[0].position;
        projectile.GetComponent<TrailRenderer>().Clear();
        projectile.GetComponent<ProjectileController>().damage = damage;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * projectileSpeed;
    }


    void ShootSpecial()
    {
        for (int i = 1; i < projectileSpawnPoint.Count; i++)
        {
            GameObject projectile = ProjectilePool.Instance.GetObject();
            projectile.transform.position = projectileSpawnPoint[i].position;
            projectile.GetComponent<TrailRenderer>().Clear();
            projectile.GetComponent<ProjectileController>().damage = damage;
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (i <= 3)
            {
                rb.velocity = transform.right * projectileSpeed;
            }
            else
            {
                rb.velocity = -transform.right * projectileSpeed;
            }
        }
    }

    public void HealthController(float damage)
    {
        healthPoints -= damage;
        healthBar.fillAmount = healthPoints / maxHealth;
        if(healthBar.fillAmount >= 0.6f)
        {
            playerSpriteRenderer.sprite = spriteList[0];
        }else if(healthBar.fillAmount >= 0.3f)
        {
            playerSpriteRenderer.sprite = spriteList[1];
        }
        else if (healthBar.fillAmount > 0f)
        {
            playerSpriteRenderer.sprite = spriteList[2];
        }
        else
        {
            playerSpriteRenderer.sprite = spriteList[3];
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        PlayerPrefs.SetInt("Score", score);
        scoreUI.text = "Score: " + score;
    }
}

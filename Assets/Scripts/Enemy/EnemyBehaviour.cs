using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private float enemySpeed = 10f;

    [SerializeField]
    private float enemyDamage = 10f;

    [SerializeField]
    private float HP = 30f;

    [SerializeField]
    private GameObject proteinDrop;

    [SerializeField]
    private int proteinCount = 3;

    [SerializeField]
    private int points = 1000;

    [SerializeField]
    private bool isInvincible = false;

    [SerializeField]
    private GameObject[] powerupDrops;

    private EnemyManager enemyManager;
    private GameObject player;
    private Rigidbody2D rb;
    private PlayerManager playerManager;
    private GameManager gameManager;

    private Vector2 playerPos;
    private Vector2 lookAt;

    private void SpawnProtein()
    {
        float randomChance = Random.Range(0f, 1f);
        if(randomChance >= 0.6f)
        {
            int randomPower = Random.Range(0, powerupDrops.Length);
            GameObject powerup = Instantiate(powerupDrops[randomPower]);
            powerup.transform.position = transform.position;
            Vector2 random = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            random = Vector2.ClampMagnitude(random, 3);
            powerup.GetComponent<Rigidbody2D>().AddForce(random, ForceMode2D.Impulse);
        }

        int randomgNumb = Random.Range(1, proteinCount + 1);
        for (int i = 0; i < randomgNumb; i++)
        {
            GameObject drops = Instantiate(proteinDrop);
            drops.transform.position = transform.position;
            Vector2 random = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            random = Vector2.ClampMagnitude(random, 3);
            drops.GetComponent<Rigidbody2D>().AddForce(random, ForceMode2D.Impulse);
        }
    }

    private void HealthCheck()
    {
        if(isInvincible == false)
        {
            if (HP <= 0)
            {
                FindObjectOfType<AudioManager>().Play("Dying SFX");
                SpawnProtein();
                gameManager.totalScore += points;
                Destroy(gameObject);
            }
        }
    }

    private void TrackPlayer()
    {
        playerPos = player.transform.position;
        lookAt = playerPos - rb.position;
        float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void MoveTowardsPlayer()
    {
        transform.position += Vector3.ClampMagnitude(transform.up * enemySpeed, enemySpeed) * Time.deltaTime;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyManager = (EnemyManager)FindObjectOfType(typeof(EnemyManager));
        rb = GetComponent<Rigidbody2D>();
        playerManager = (PlayerManager)FindAnyObjectByType(typeof(PlayerManager));
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
    }

    private void Update()
    {

        HealthCheck();
        TrackPlayer();
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isInvincible == false)
        {
            if (collision.tag == "Player Bullet")
            {
                HP -= playerManager.getPlayerDamage();
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerManager playerManager = collision.GetComponent<PlayerManager>();
            if (playerManager.damaged == false)
            {
                float currenthp = playerManager.getCurrentHP();
                playerManager.setCurrentHP(currenthp -= enemyDamage);
                playerManager.StartCoroutine(playerManager.invincibilityFrames());
            }
        }
    }
}

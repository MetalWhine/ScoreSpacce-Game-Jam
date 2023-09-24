using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField][Tooltip("Set damage for player bullets")]
    private float playerDamage = 10;
    [SerializeField][Tooltip("Maximum HP")]
    private float maxHP = 100;
    [SerializeField][Tooltip("Current HP")]
    private float currentHP = 100;
    [SerializeField][Tooltip("How fast player's HP drain")]
    private float energyDrain = 1;
    [SerializeField][Tooltip("How long invincibility frames after getting damaged")]
    private float invincibilityDuration = 1;

    public bool damaged = false;
    private GameManager gameManager;

    private PlayerMove playerMove;
    private PlayerShoot playerShoot;

    public IEnumerator invincibilityFrames()
    {
        damaged = true;
        yield return new WaitForSeconds(invincibilityDuration);
        damaged = false;
    }

    private void CheckHealth()
    {
        if(currentHP <= 0)
        {
            gameManager.playerDies();
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerShoot = GetComponent<PlayerShoot>();
        currentHP = maxHP;
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
    }

    public void setPlayerDamage(float dmg)
    {
        playerDamage = dmg;
    }

    public float getPlayerDamage()
    {
        return playerDamage;
    }

    public void setMaxHP(float maxHealth)
    {
        maxHP = maxHealth;
    }

    public float getMaxHP()
    {
        return maxHP;
    }

    public void setCurrentHP(float HP)
    {
        currentHP = Mathf.Clamp(HP, 0, maxHP);
    }

    public float getCurrentHP()
    {
        return currentHP;
    }

    private void Update()
    {
        currentHP -= energyDrain * Time.deltaTime;
        CheckHealth();
    }
}

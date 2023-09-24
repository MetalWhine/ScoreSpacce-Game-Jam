using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProteinScript : MonoBehaviour
{
    [SerializeField]
    private float energyValue = 1;
    [SerializeField]
    private int pointValue = 200;
    [SerializeField]
    private Sprite superProtein;

    private GameManager gameManager;

    private bool super = false;

    private void Start()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            float CurrentHealth = player.getCurrentHP();
            player.setCurrentHP(CurrentHealth += energyValue);
            gameManager.totalScore += pointValue;
            Destroy(gameObject);
        }
        if (collision.tag == "Macrophage")
        {
            if(super == false)
            {
                GetComponentInChildren<SpriteRenderer>().sprite = superProtein;
                pointValue = pointValue * 10;
                super = true;
            }
        }
    }
}

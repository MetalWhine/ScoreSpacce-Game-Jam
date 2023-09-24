using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoseScreenScript : MonoBehaviour
{

    private Animator animator;
    private GameManager gameManager;

    [SerializeField]
    private TMP_Text score;

    [SerializeField]
    private TMP_Text localLeaderboard;

    [SerializeField]
    private TMP_Text globalLeaderboad;

    private void Start()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        animator = GetComponent<Animator>();
    }

    public void gameOverScreen()
    {
        animator.SetTrigger("Game Over");
        score.text = gameManager.totalScore.ToString();
        globalLeaderboad.text = gameManager.getHighLeaderboard();
    }

}

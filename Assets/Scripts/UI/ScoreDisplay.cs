using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private TMP_Text TMPtext;
    private GameManager gameManager;


    private void Start()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        TMPtext = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        TMPtext.text = gameManager.totalScore.ToString();
    }
}

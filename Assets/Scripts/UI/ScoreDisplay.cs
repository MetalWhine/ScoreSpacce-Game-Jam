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
        TMPtext = GetComponent<TMP_Text>();
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
    }

    private void Update()
    {
        TMPtext.text = gameManager.totalScore.ToString();
    }
}

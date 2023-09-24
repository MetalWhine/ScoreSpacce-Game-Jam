using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubmitScore : MonoBehaviour
{
    private TMP_InputField inputField;
    private GameManager gameManager;

    private void Start()
    {
        inputField =  GetComponentInChildren<TMP_InputField>();
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
    }

    public void inputScore()
    {
        gameManager.uploadScoretoLeaderboards(gameManager.getGuestID(), int.Parse(inputField.text));
    }
}

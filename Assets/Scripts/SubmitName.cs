using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmitName : MonoBehaviour
{
    TMP_InputField nameInput;

    private void Start()
    {
        nameInput = GetComponentInChildren<TMP_InputField>();
        nameInput.text = GameManager.playerName;
    }

    public void updateName()
    {
        GameManager.playerName = nameInput.text;
    }
}

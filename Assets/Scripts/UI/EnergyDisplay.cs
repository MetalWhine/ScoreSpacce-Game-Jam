using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnergyDisplay : MonoBehaviour
{

    private Slider slider;
    private PlayerManager playerManager;

    private void Start()
    {
        slider = GetComponent<Slider>();
        playerManager = (PlayerManager)FindObjectOfType(typeof(PlayerManager));
    }

    void Update()
    {
        slider.maxValue = playerManager.getMaxHP();
        slider.value = playerManager.getCurrentHP();
    }
}

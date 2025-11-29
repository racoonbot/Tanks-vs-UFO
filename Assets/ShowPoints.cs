using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ShowPoints : MonoBehaviour
{
    public Wallet wallet;
    public TextMeshProUGUI text;

    private void Awake()
    {
        if (wallet != null) 
        wallet.OnLevelMoneyChange += UpdateInGamePoints;
    }

    private void OnDisable()
    {
        wallet.OnLevelMoneyChange -= UpdateInGamePoints;
    }

    public void UpdateInGamePoints(int levelMoney)
    {
        text.text = $"{levelMoney}\n";
    }
}
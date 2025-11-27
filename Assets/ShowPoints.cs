using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowPoints : MonoBehaviour
{
    public Wallet wallet;
    public TextMeshProUGUI text;

    private void Start()
    {
        UpdateInGamePoints();
    }

    public void UpdateInGamePoints()
    {
        if (wallet != null)
        {
            text.text = $"{wallet.LevelMoney}\n";
        }
        else
        {
            Debug.LogError("Wallet не назначен!");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using TMPro;

public class ShowMoney : MonoBehaviour
{
    public Wallet wallet;
    public TextMeshProUGUI text;

  

    public void UpdateText()
    {
        if (wallet != null)
        {
            text.text = $"Монеты : {wallet.LevelMoney}\n";
        }
        else
        {
            Debug.LogError("Wallet не назначен!");
        }
    }
}
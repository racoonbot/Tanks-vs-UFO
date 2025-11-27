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
            text.text = $"Заработано за уровень: {wallet.LevelMoney}\nВсего денег: {wallet.TotalMoney}";
        }
        else
        {
            Debug.LogError("Wallet не назначен!");
        }
    }
}
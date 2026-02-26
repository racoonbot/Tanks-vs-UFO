using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class ShowFinalMoney : MonoBehaviour
{
    public TextMeshProUGUI finalMoneyText;
    public Wallet wallet;
    public TankHealth _tankHealth;


    private void Start()
    {
        if (_tankHealth != null)
        {
            _tankHealth.OnDeathPlayer += UpdateText;
        }
       
    }

    private void OnDisable()
    {
        _tankHealth.OnDeathPlayer -= UpdateText;

    }

    private void UpdateText()
    {
        string label = (YG2.lang == "ru") ? "Монет заработано: " : "Total Money: ";
        finalMoneyText.text = label + wallet.totalMoney.ToString();
    }

}
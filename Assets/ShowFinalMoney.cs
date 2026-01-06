using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

        finalMoneyText.text = $"Монет заработано:" + wallet.totalMoney.ToString();
    }
}
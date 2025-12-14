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
        else
        {
            Debug.Log("No tank health");
        }

        Debug.Log("Awake");
    }

    private void OnDisable()
    {
        _tankHealth.OnDeathPlayer -= UpdateText;
        Debug.Log("OnDisable");
    }

    private void UpdateText()
    {
        Debug.Log("UpdateText method called!");
        finalMoneyText.text = $"Монет заработано:" + wallet.totalMoney.ToString();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int TotalMoney;

    public event Action<int> OnLevelMoneyChange;
    
    
    private int levelMoney;
    public int LevelMoney
    {
        get { return levelMoney; }
        private set
        {
            levelMoney = value;
            OnLevelMoneyChange?.Invoke(levelMoney);
   
        }
    }


    private void Start()
    {
        LevelMoney = 0;
    }

    public void AddMoney(int reward) //добавляет деньги за врага в течении уровня
    {
        LevelMoney += reward;
    }

    // public void AddTotalMoney(int levelMoney) // В конце уровня добавляет деньги за уровень в общему кошельку
    // {
    //     TotalMoney += levelMoney;
    // }

    public void RemoveMoney(int salePrice) // для покупок
    {
        TotalMoney -= salePrice;
    }
}
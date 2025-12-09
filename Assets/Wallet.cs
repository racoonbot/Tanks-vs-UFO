using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    // public int TotalMoney;

    public event Action<int> OnLevelMoneyChange;
    
    
    private int levelMoney;
    public int LevelMoney
    {
        get { return levelMoney; }
        set
        {
            levelMoney = value;
            OnLevelMoneyChange?.Invoke(levelMoney);
   
        }
    }


    private void Start()
    {
        LevelMoney = 0;
    }

    public void AddMoney(int reward) 
    {
        LevelMoney += reward;
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
   
   
    public int TotalMoney;
   



    public void AddMoney(int reward)
    {
        TotalMoney += reward;
    }
}
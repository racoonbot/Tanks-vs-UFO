using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YG; 
public class LeaderBoard : MonoBehaviour
{
   private TankHealth tankHealth;
   private Wallet wallet;

   private void Start()
   {
       tankHealth = FindObjectOfType<TankHealth>();
       wallet = FindObjectOfType<Wallet>();
       if (wallet == null)
       {
           Debug.Log("Wallet not found");
       }
       if (tankHealth != null)
       {
           tankHealth.OnDeathPlayer += SaveMyScore;
       }
   }

   private void OnDisable()
   {
       tankHealth.OnDeathPlayer -= SaveMyScore;
   }
   public void SaveMyScore()
   {
       Debug.Log("SaveMyScore");
       int finalScore = wallet.totalMoney; 
       YG2.SetLeaderboard("testlb", finalScore);
   }
}


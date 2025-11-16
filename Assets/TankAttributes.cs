using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttributes : MonoBehaviour
{
   public float maxSpeed;
   public int maxHealth;
   public int damage;


   public void AddMaxSpeed()
   {
      maxSpeed = maxSpeed * 2;
   }
   
   public void AddMaxHealth(int amount)
   {
      maxHealth += amount;
   }

   public void AddDamage(int amount)
   {
      damage += amount;
   }
   
}

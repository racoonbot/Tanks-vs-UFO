using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttributes : MonoBehaviour
{
   public float maxSpeed;
   public int maxHealth;
   public int damage;
   public float turretRotationSpeed;


   public void AddMaxSpeed()
   {
      maxSpeed *= 0.1f;
   }
   
   public void AddMaxHealth()
   {
      maxHealth += 1;
   }

   public void AddDamage(int amount)
   {
      damage += amount;
   }

   public void AddTurretRotationSpeed()
   {
      turretRotationSpeed *= 0.1f;
   }
   
}

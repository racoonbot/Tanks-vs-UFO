using System.Collections;
using UnityEngine;

public class BustHeal : Loot
{
    public int minAmount = 1;
    public int maxAmount = 10;

    protected override void ActivateBust()
    {
        GainHealth(); 
    }

    private void GainHealth()
    {
        TankHealth tankHealth = FindObjectOfType<TankHealth>();

        if (tankHealth != null)
        {
            tankHealth.Heal(RandomHealAmount());
  
        }
       
    }

    private int RandomHealAmount()
    {
        return Random.Range(minAmount, maxAmount);
    }
}
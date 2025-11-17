using System.Collections;
using UnityEngine;

public class BustHeal : Loot
{
    public int healAmount = 1; 

    protected override void ActivateBust()
    {
        GainHealth(); 
    }

    private void GainHealth()
    {
        TankHealth tankHealth = FindObjectOfType<TankHealth>();

        if (tankHealth != null)
        {
            tankHealth.Heal(healAmount);
            Debug.Log($"Health increased by {healAmount}.");
        }
        else
        {
            Debug.LogWarning("TankHealth component not found!");
        }
    }
}
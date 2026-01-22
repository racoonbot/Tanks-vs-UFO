using UnityEngine;

public class BustHeal : Loot
{
    public int minAmount = 1;
    public int maxAmount = 11;

    protected override void ActivateBust()
    {
        GainHealth(); 
    }

    private void GainHealth()
    {
        TankHealth tankHealth = FindObjectOfType<TankHealth>();
        TankAttributes tankAttributes = FindObjectOfType<TankAttributes>();

        if (tankHealth != null && tankAttributes != null)
        {
            int healAmount = RandomHealAmount();
            tankHealth.health = Mathf.Min(tankHealth.health + healAmount, tankAttributes.maxHealth);
        }
    }

    private int RandomHealAmount()
    {
        return Random.Range(minAmount, maxAmount);
    }
}
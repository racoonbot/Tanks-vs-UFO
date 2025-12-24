using UnityEngine;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    public UpgradeItem item;
    public TankAttributes player;
    public Wallet wallet;
    public TextMeshProUGUI text;
    private TankHealth _tankHealth;

    private void Start()
    {
        UpdateText();
        _tankHealth = FindObjectOfType<TankHealth>();
    }

    public void BuyItem()
    {
        int price = GetCurrentPrice();

        if (wallet.LevelMoney >= price)
        {
            wallet.LevelMoney -= price;
            
            player.ApplyUpgrade(item.type, item.amount);
            if (item.type.ToString().Contains("Health"))
            {
                _tankHealth.health = player.maxHealth;
            }

            int currentLevel = PlayerPrefs.GetInt(item.type.ToString(), 0);
            PlayerPrefs.SetInt(item.type.ToString(), currentLevel + 1);
            PlayerPrefs.Save();

            UpdateText();
        }
    }

    public void UpdateText()
    {
        text.text = $"{GetCurrentPrice()}";
    }

    private int GetCurrentPrice()
    {
        int currentLevel = PlayerPrefs.GetInt(item.type.ToString(), 0);
        int exponentialGrowth = currentLevel * currentLevel * 2; 
    
        return item.price + exponentialGrowth;
    }
}
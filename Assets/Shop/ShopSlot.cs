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
        _tankHealth = FindObjectOfType<TankHealth>();
        UpdateText();
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
    
    
    public void ResetItemLevel()
    {
        // Удаляем ключ конкретно для этого типа предмета
        PlayerPrefs.DeleteKey(item.type.ToString());
    
        // Обязательно сохраняем изменения на диск
        PlayerPrefs.Save(); 

        // Обновляем текст цены, чтобы он вернулся к базовому значению
        UpdateText();
    
        Debug.Log($"Прогресс для {item.type} был сброшен.");
    }
    
    
}
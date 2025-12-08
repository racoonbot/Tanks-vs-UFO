
using UnityEngine;
using TMPro;
public class ShopSlot : MonoBehaviour
{
    public UpgradeItem item;
    public TankAttributes player;
    public Wallet wallet;
    public TextMeshProUGUI text;

    private void Start()
    {
        UpdateText();
    }

    public void BuyItem()
    {
        if (item.price <= wallet.LevelMoney)
        {
           wallet.LevelMoney -= item.price;
           player.ApplyUpgrade(item.type,  item.amount);
           item.price += 10;
           UpdateText();
        }
        
    }

    public void UpdateText()
    {
        text.text = $"{item.type}\n{item.price}";
    }
}

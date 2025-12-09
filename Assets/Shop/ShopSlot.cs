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
        // 1. Сначала узнаем текущую цену (вычисляем её)
        int price = GetCurrentPrice();

        // 2. Проверяем, хватает ли денег на ВЫЧИСЛЕННУЮ цену
        if (wallet.LevelMoney >= price)
        {
            // Списываем деньги
            wallet.LevelMoney -= price;
            
            // Применяем улучшение
            player.ApplyUpgrade(item.type, item.amount);

            // 3. САМОЕ ВАЖНОЕ: Сохраняем прогресс
            // Получаем текущий уровень прокачки
            int currentLevel = PlayerPrefs.GetInt(item.type.ToString(), 0);
            
            // Увеличиваем уровень на 1 и записываем обратно
            PlayerPrefs.SetInt(item.type.ToString(), currentLevel + 1);
            
            // Принудительно сохраняем данные на диск
            PlayerPrefs.Save();

            // Обновляем текст
            UpdateText();
        }
    }

    public void UpdateText()
    {
        // В тексте тоже показываем не базовую, а актуальную (вычисленную) цену
        text.text = $"{item.type}: {GetCurrentPrice()}";
    }

    // Вспомогательный метод, чтобы считать цену в одном месте
    private int GetCurrentPrice()
    {
        // Спрашиваем у сохранения: "Сколько раз мы уже купили этот товар?"
        // (item.type.ToString() превратит тип "MaxHealth" в текст "MaxHealth")
        // 0 — это значение по умолчанию, если мы еще ничего не покупали
        int currentLevel = PlayerPrefs.GetInt(item.type.ToString(), 0);

        // Формула: Базовая цена + (10 монет за каждый уровень)
        // Ты можешь поменять 10 на любое другое число
        int finalPrice = item.price + (currentLevel * 10);

        return finalPrice;
    }
}
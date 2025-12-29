using UnityEngine;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    public UpgradeItem item;
    public TankAttributes player;
    public Wallet wallet;
    public TextMeshProUGUI text;
    private TankHealth _tankHealth;
    public AudioSource audioSource;

    private void Start()
    {
        _tankHealth = FindObjectOfType<TankHealth>();
        UpdateText();
    }


    public void BuyItem()
    {
        int price = GetCurrentPrice();

        // 1. Сначала определяем StatType, чтобы знать, какой лимит проверять и какая кнопка должна мигать
        StatType currentStat = StatType.Health;
        if (item.type.ToString().Contains("Speed")) currentStat = StatType.Speed;
        else if (item.type.ToString().Contains("Rotation")) currentStat = StatType.Rotation;
        else if (item.type.ToString().Contains("Health")) currentStat = StatType.Health;

        // 2. ПРОВЕРКА НА ЛИМИТ (Самый высокий приоритет)
        bool isLimitReached = false;

        if (currentStat == StatType.Health && player.maxHealth >= player.healthLimit) isLimitReached = true;
        else if (currentStat == StatType.Speed && player.maxSpeed >= player.speedLimit) isLimitReached = true;
        else if (currentStat == StatType.Rotation && player.turretRotationSpeed >= player.turretRotationSpeedLimit)
            isLimitReached = true;

        if (isLimitReached)
        {
            // Если лимит достигнут - вызываем мигание и ВЫХОДИМ из метода
            player.OnMaximumLevelReached?.Invoke(currentStat);
            Debug.Log($"<color=yellow>Улучшение {currentStat} невозможно: достигнут лимит!</color>");
            return; // Этот return не даст коду идти дальше к покупке
        }

        // 3. ПРОВЕРКА НА ДЕНЬГИ (Только если лимит еще не достигнут)
        if (wallet.LevelMoney >= price)
        {
            wallet.LevelMoney -= price;
            audioSource.Play();

            player.ApplyUpgrade(item.type, item.amount);

            // Обновляем текущее здоровье танка, если качали макс. здоровье
            if (currentStat == StatType.Health)
            {
                if (_tankHealth == null) _tankHealth = FindObjectOfType<TankHealth>();
                _tankHealth.health = player.maxHealth;
            }

            // Сохранение уровня прокачки
            int currentLevel = PlayerPrefs.GetInt(item.type.ToString(), 0);
            PlayerPrefs.SetInt(item.type.ToString(), currentLevel + 1);
            PlayerPrefs.Save();

            UpdateText();
        }
        else
        {
            // Если лимит не достигнут, но денег не хватает - тоже мигаем
            player.OnMaximumLevelReached?.Invoke(currentStat);
            Debug.Log("<color=red>Недостаточно денег!</color>");
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
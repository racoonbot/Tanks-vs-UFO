using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Shop/Upgrade Item")]
public class UpgradeItem : ScriptableObject // <-- Обрати внимание, не MonoBehaviour!
{
    public string title; // Название, которое увидит игрок
    public int price;    // Сколько стоит апгрейд
    public UpgradeType type;
    public float amount;
}
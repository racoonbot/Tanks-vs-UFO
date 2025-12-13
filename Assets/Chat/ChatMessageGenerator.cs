using UnityEngine;
using System.Collections.Generic;

public class ChatMessageGenerator : MonoBehaviour
{
    private string[] phrases = new string[]
    {
        "Враг замечен на севере!",
        "Нужен хил!",
        "Где дроп?",
        "Погнали на босса",
        "AFK 5 мин",
        "Куплю меч +10",
        "Кто в пати?",
        "GG WP"
    };

    private string[] nicknames = new string[] { "Зеленый", "Желтый", "Красный"};

    public string Generate()
    {
        string nick = nicknames[Random.Range(0, nicknames.Length)];
        string msg = phrases[Random.Range(0, phrases.Length)];
        string colorHex = ColorUtility.ToHtmlStringRGB(Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));

        // Возвращаем строку с раскрашенным ником (TMP поддерживает HTML теги)
        return $"<color=#{colorHex}><b>{nick}:</b></color> {msg}";
    }
}
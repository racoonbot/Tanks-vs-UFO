using UnityEngine;

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
        "GG WP",
        "Ай, больно!",
        "Это был мисклик!",
        "Лаги!"
    };

    public string Generate(string nick, Color color)
    {
        string msg = phrases[Random.Range(0, phrases.Length)];
        string colorHex = ColorUtility.ToHtmlStringRGB(color);
        return $"<color=#{colorHex}><b>{nick}:</b></color> {msg}";
    }
}
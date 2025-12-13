using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatController : MonoBehaviour
{
    public static ChatController Instance; // 1. Делаем доступ глобальным

    [Header("Ссылки")]
    public ChatMessageGenerator generator; // Ссылка на ваш генератор
    public Transform contentContainer;
    public GameObject messagePrefab;
    public ScrollRect scrollRect;

    private List<GameObject> messagesList = new List<GameObject>();
    private int maxMessages = 20;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowEnemyHitMessage()
    {
        // 1. Просим генератор дать нам фразу
        string text = generator.Generate(); 
        
        // 2. Выводим её в чат
        AddMessageToChat(text);
    }

    private void AddMessageToChat(string text)
    {
        GameObject newMsg = Instantiate(messagePrefab, contentContainer);
        newMsg.GetComponent<TextMeshProUGUI>().text = text;

        messagesList.Add(newMsg);
        if (messagesList.Count > maxMessages)
        {
            Destroy(messagesList[0]);
            messagesList.RemoveAt(0);
        }

        StartCoroutine(AutoScroll());
    }

    System.Collections.IEnumerator AutoScroll()
    {
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
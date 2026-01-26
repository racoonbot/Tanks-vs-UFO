using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;


public class ChatController : MonoBehaviour
{
    public static ChatController Instance;

    [Header("Ссылки")]
    public ChatMessageGenerator generator;
    public EnChatMessageGenerator enGenerator;
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

    public void ShowEnemyHitMessage(string nickName, Color color)
    {
        string text = "";
        if (YG2.lang == "ru" && generator != null)
        {
            text = generator.Generate(nickName, color);
        }
        else  if (YG2.lang == "en" && generator != null)
        {
            text = enGenerator.Generate(nickName, color);
        }
        else
        {
            text = enGenerator.Generate(nickName, color);
        }
        
        if (string.IsNullOrEmpty(text)) return;
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
        Canvas.ForceUpdateCanvases(); 
        scrollRect.verticalNormalizedPosition = 0f; 
        
        yield return new WaitForEndOfFrame(); 
        scrollRect.verticalNormalizedPosition = 0f; 
    }
}
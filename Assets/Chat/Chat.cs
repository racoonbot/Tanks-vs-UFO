using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chat : MonoBehaviour
{
    private TextMeshProUGUI chatText;
    public ChatMessageGenerator MessageGenerator;

    void Awake()
    {
        chatText = GetComponent<TextMeshProUGUI>();
        MessageGenerator = GetComponent<ChatMessageGenerator>();
    }
    void Start()
    {
        SendMessages();
    }


    public void SendMessages()
    {
        // chatText.text = MessageGenerator.Generate();
    }
}

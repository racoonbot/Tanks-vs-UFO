using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowCurrentSpeed : MonoBehaviour
{
    public Tank tank;
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = tank.currentSpeed.ToString("F1");
    }
}

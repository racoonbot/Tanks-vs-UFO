using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowMaxSpeed : MonoBehaviour
{
    public TankAttributes tankAttributes;
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = tankAttributes.maxSpeed.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowRotationSpeed : MonoBehaviour
{
    public TankAttributes tankAttributes;
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = tankAttributes.turretRotationSpeed.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowCurrentHealth : MonoBehaviour
{
    public TankHealth tankHealth;
    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        text.text = tankHealth.health.ToString();
    }
}

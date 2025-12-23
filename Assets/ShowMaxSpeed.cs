using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowMaxSpeed : MonoBehaviour
{
    public TankAttributes tankAttributes;
    public Rigidbody tankRigidbody; // Нужно перетащить Rigidbody танка в инспекторе
    public TextMeshProUGUI text;

    void Update()
    {
        // Рассчитываем физический предел скорости
        // Формула: (Сила * FixedDeltaTime) / (Масса * Сопротивление)
        float drag = tankRigidbody.drag;
        float mass = tankRigidbody.mass;
        
        if (drag > 0) 
        {
            float calculatedMaxSpeed = ((tankAttributes.maxSpeed * Time.fixedDeltaTime) / (mass * drag)) - 0.1f;
            text.text = calculatedMaxSpeed.ToString("F2");
        }
        else 
        {
            text.text = "Infinity"; // Без Drag скорость не ограничена
        }
    }
}
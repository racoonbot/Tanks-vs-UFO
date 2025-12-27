using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowMaxSpeed : MonoBehaviour
{
    public TankAttributes tankAttributes;
    public Rigidbody tankRigidbody;
    public TextMeshProUGUI text;

    void Update()
    {
        float drag = tankRigidbody.drag;
        float mass = tankRigidbody.mass;
        
        if (drag > 0) 
        {
            float calculatedMaxSpeed = ((tankAttributes.maxSpeed * Time.fixedDeltaTime) / (mass * drag)) - 0.1f;
            text.text = calculatedMaxSpeed.ToString("F1");
        }
        else 
        {
            text.text = "Infinity"; 
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Turret : MonoBehaviour
{
    public float rotationSpeed = 20f;
    public Transform turretPivotPoint;
    private Rigidbody rb; 
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.position =  turretPivotPoint.position;
    }

    void FixedUpdate() 
    {
        
        float mouseInput = Input.GetAxis("Mouse X");

        if (mouseInput != 0)
        {
            float rotationAmount = mouseInput * rotationSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotationAmount, 0f)); // Применяем вращение
        }
    }
}
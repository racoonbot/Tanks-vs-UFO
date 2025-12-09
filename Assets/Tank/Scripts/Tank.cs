using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{

    
    private TankAttributes attributes; 
    
    public float rotationSpeed = 5f;
    public Rigidbody rb;
    private bool isForwardDirection;
    public float currentSpeed;

    void Awake()
    {
        attributes = FindObjectOfType<TankAttributes>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float currentForce = attributes.maxSpeed; 

        if (Input.GetAxis("Vertical") > 0)
        {
            isForwardDirection = true;
            rb.AddForce(transform.forward * currentForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            isForwardDirection = false;
            rb.AddForce(-transform.forward * currentForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }

        // ... остальной код поворота без изменений ...
        if (Input.GetAxis("Horizontal") > 0)
        {
            if (isForwardDirection)
            {
                rb.MoveRotation(Quaternion.Euler(0f, rotationSpeed * Time.fixedDeltaTime, 0f) * rb.rotation);
            }
            else
            {
                rb.MoveRotation(Quaternion.Euler(0f, -rotationSpeed * Time.fixedDeltaTime, 0f) * rb.rotation);
            }
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            if (isForwardDirection)
            {
                rb.MoveRotation(Quaternion.Euler(0f, -rotationSpeed * Time.fixedDeltaTime, 0f) * rb.rotation);
            }
            else
            {
                rb.MoveRotation(Quaternion.Euler(0f, rotationSpeed * Time.fixedDeltaTime, 0f) * rb.rotation);
            }
        }
        currentSpeed = rb.velocity.magnitude;
    }
}
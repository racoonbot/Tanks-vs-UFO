using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField]
    private float force;
    
    private TankAttributes attributes;
    
    public float rotationSpeed = 5f;
    public Rigidbody rb;
    private bool isForwardDirection;
    

    void Start()
    {
        attributes = FindObjectOfType<TankAttributes>();
        force = attributes.maxSpeed;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            isForwardDirection = true;
            rb.AddForce(transform.forward * force * Time.fixedDeltaTime, ForceMode.Acceleration);
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            isForwardDirection = false;
            rb.AddForce(-transform.forward * force * Time.fixedDeltaTime, ForceMode.Acceleration);
        }

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
    }
}
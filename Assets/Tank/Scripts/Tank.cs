using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public float force = 500f;
    public float rotationSpeed = 5f;
    private Rigidbody rb;
    private bool isForwardDirection;

    void Start()
    {
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
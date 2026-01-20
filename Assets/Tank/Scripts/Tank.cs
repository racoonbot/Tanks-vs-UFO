using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG; // Добавляем для доступа к YG2.envir

public class Tank : MonoBehaviour
{
    private TankAttributes attributes; 
    public float rotationSpeed = 5f;
    public Rigidbody rb;
    private bool isForwardDirection;
    public float currentSpeed;

    [Header("Настройки Джойстиков")]
    public Joystick moveJoystick;    
    public Joystick turretJoystick;  
    
    [Header("Башня")]
    public Transform turretTransform; 
    public float turretRotationSpeed = 50f;

    void Awake()
    {
        attributes = FindObjectOfType<TankAttributes>();
        rb = GetComponent<Rigidbody>();
    }
    void Start() { }

    void FixedUpdate()
    {
        float currentForce = attributes.maxSpeed; 

       // float v = Input.GetAxis("Vertical");
       // float h = Input.GetAxis("Horizontal");
       // float tH = Input.GetAxis("Mouse X");

       float v = 0;
       float h= 0;
       float tH= 0;
       
      //  if (YG2.envir.isMobile || YG2.envir.isTablet)
       // {
            if (moveJoystick != null)
            {
                v = moveJoystick.Vertical;
                h = moveJoystick.Horizontal;
               v = Mathf.RoundToInt(v);
               h = Mathf.RoundToInt(h);
            }
            if (turretJoystick != null)
            {
                tH = turretJoystick.Horizontal;
                Debug.Log("tH"+ tH );
            }
      // }
        if (v > 0)
        {
            isForwardDirection = true;
            rb.AddForce(transform.forward * currentForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        else if (v < 0)
        {
            isForwardDirection = false;
            rb.AddForce(-transform.forward * currentForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        
        if (h > 0)
        {
            if (isForwardDirection)
                rb.MoveRotation(Quaternion.Euler(0f, rotationSpeed * Time.fixedDeltaTime, 0f) * rb.rotation);
            else
                rb.MoveRotation(Quaternion.Euler(0f, -rotationSpeed * Time.fixedDeltaTime, 0f) * rb.rotation);
        }
        else if (h < 0)
        {
            if (isForwardDirection)
                rb.MoveRotation(Quaternion.Euler(0f, -rotationSpeed * Time.fixedDeltaTime, 0f) * rb.rotation);
            else
                rb.MoveRotation(Quaternion.Euler(0f, rotationSpeed * Time.fixedDeltaTime, 0f) * rb.rotation);
        }

        // --- ЛОГИКА ПОВОРОТА БАШНИ ---
        if (turretTransform != null && Mathf.Abs(tH) > 0.05f)
        {
            turretTransform.Rotate(0f, tH * turretRotationSpeed * Time.fixedDeltaTime, 0f);
        }

        currentSpeed = rb.velocity.magnitude;
    }
}
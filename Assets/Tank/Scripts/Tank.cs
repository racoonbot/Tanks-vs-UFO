using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG; 

public class Tank : MonoBehaviour
{
    private TankAttributes attributes;
    public float rotationSpeed = 5f;
    public Rigidbody rb;
    private bool isForwardDirection;
    public float currentSpeed;

    [Header("Настройки Джойстиков")] 
    public Joystick moveJoystick;

    // Ссылка на башню здесь больше не нужна для вращения, 
    // но ее можно оставить, если она нужна для других целей (например, стрельбы)
    [Header("Башня")] 
    public Transform turretTransform;

    void Awake()
    {
        attributes = FindObjectOfType<TankAttributes>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (attributes == null) return;

        float currentForce = attributes.maxSpeed;

        // Ввод для движения корпуса
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        
        // Работа с джойстиком только для перемещения корпуса
        if (moveJoystick != null)
        {
            if (Mathf.Abs(v) < 0.01f) 
            {
                v = moveJoystick.Vertical;
                v = Mathf.RoundToInt(v);
            }

            if (Mathf.Abs(h) < 0.01f) 
            {
                h = moveJoystick.Horizontal;
                h = Mathf.RoundToInt(h);
            }
        }

        // Логика движения вперед/назад
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

        // Логика поворота корпуса (зависит от направления движения)
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

        // ВЕСЬ БЛОК ВРАЩЕНИЯ БАШНИ УДАЛЕН ОТСЮДА
        // Теперь за это отвечает отдельный скрипт Tank_Turret

        currentSpeed = rb.velocity.magnitude;
    }
}
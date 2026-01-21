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

    [Header("Настройки Джойстиков")] public Joystick moveJoystick;
    public Joystick turretJoystick;

    [Header("Башня")] public Transform turretTransform;
    // public float turretRotationSpeed;

    void Awake()
    {
        attributes = FindObjectOfType<TankAttributes>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
    }

    void FixedUpdate()
    {
        float currentForce = attributes.maxSpeed;

        // 1. Считываем ввод с клавиатуры (WASD / Стрелки)
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        
        // Считываем мышь для башни (опционально, если нужно управление мышью на ПК)
        // Если хотите поворот башни на клавиши Q и E, замените "Mouse X" на свои оси или кнопки.
        float tH = Input.GetAxis("Mouse X"); 

        // 2. Если ввод с клавиатуры равен 0, пытаемся считать Джойстики
        // (Это позволяет играть и на ПК, и на телефоне без смены кода)
        
        if (moveJoystick != null)
        {
            // Если игрок не жмет кнопки на клавиатуре (v == 0), берем значение с джойстика
            if (Mathf.Abs(v) < 0.01f) 
            {
                v = moveJoystick.Vertical;
                // v = Mathf.RoundToInt(v); // Раскомментируйте, если нужно резкое движение без плавного разгона
            }

            if (Mathf.Abs(h) < 0.01f) 
            {
                h = moveJoystick.Horizontal;
                // h = Mathf.RoundToInt(h); // То же самое для поворота
            }
        }

        if (turretJoystick != null)
        {
            // Если мышь не двигается, берем значение с джойстика башни
            if (Mathf.Abs(tH) < 0.01f)
            {
                tH = turretJoystick.Horizontal;
            }
        }

        // --- ДАЛЕЕ ВАША ФИЗИКА БЕЗ ИЗМЕНЕНИЙ ---

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
            turretTransform.Rotate(0f, tH * attributes.turretRotationSpeed * Time.fixedDeltaTime, 0f);
        }

        currentSpeed = rb.velocity.magnitude;
    }
}
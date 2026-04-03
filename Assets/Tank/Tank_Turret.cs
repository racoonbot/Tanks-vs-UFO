using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Tank_Turret : MonoBehaviour
{
    [Header("Настройки привязки")]
    public Transform turretPivotPoint; // Точка на корпусе танка
    
    private Rigidbody rb;
    private TankAttributes attributes;

    // Направление вращения в текущем кадре
    private float rotateDirection = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        attributes = FindObjectOfType<TankAttributes>();

        // Включаем кинематику, чтобы физика не мешала программному вращению
        rb.isKinematic = true;
    }

    void Update()
    {
        // 1. Привязка позиции к корпусу
        if (turretPivotPoint != null)
        {
            transform.position = turretPivotPoint.position;
        }

        // 2. Считываем ввод мыши
        float mouseInput = Input.GetAxisRaw("Mouse X");

        // 3. ОПРЕДЕЛЕНИЕ НАПРАВЛЕНИЯ (Без сохранения состояния)
        if (mouseInput > 0.001f) 
        {
            rotateDirection = 1f; // Движемся вправо
        }
        else if (mouseInput < -0.001f) 
        {
            rotateDirection = -1f; // Движемся влево
        }
        else 
        {
            rotateDirection = 0f; // Остановка, если мышь не движется
        }
    }

    void FixedUpdate()
    {
        // Если башня не должна вращаться или нет данных о скорости — выходим
        if (attributes == null || rotateDirection == 0) return;

        // 4. ПРИМЕНЕНИЕ ВРАЩЕНИЯ
        // Используем только направление (1 или -1) и фиксированную скорость
        float rotationStep = rotateDirection * attributes.turretRotationSpeed * Time.fixedDeltaTime;

        // Создаем и применяем поворот
        Quaternion deltaRotation = Quaternion.Euler(0f, rotationStep, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
using UnityEngine;

public class TankWheel : MonoBehaviour
{
    public float rotationSpeed = 100f; // Скорость вращения

    void Update()
    {
        // Получаем направление движения автомобиля (скорость)
        float movement = Input.GetAxis("Vertical"); // Используем входные данные

        // Условие вращения колес
        if (movement > 0) // Двигается вперед
        {
            RotateWheel(-rotationSpeed);
        }
        else if (movement < 0) // Двигается назад
        {
            RotateWheel(rotationSpeed);
        }
    }

    void RotateWheel(float rotationAmount)
    {
        // Вращение колеса вокруг оси Y
        transform.Rotate(rotationAmount, 0f * Time.deltaTime, 0f);
    }
  
}
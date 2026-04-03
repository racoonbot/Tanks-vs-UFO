using UnityEngine;

public class TankWheel : MonoBehaviour
{
    [Header("Настройки вращения")]
    public float rotationSpeed = 100f; // Скорость вращения колеса

    void Update()
    {
        // Получаем ввод с клавиатуры (W/S или стрелочки вверх/вниз)
        // Значение будет от -1 до 1
        float movementKeyboard = Input.GetAxis("Vertical"); 

        // Если нажата клавиша "Вперед" (W / Вверх)
        if (movementKeyboard > 0) 
        {
            RotateWheel(-rotationSpeed);
        }
        // Если нажата клавиша "Назад" (S / Вниз)
        else if (movementKeyboard < 0) 
        {
            RotateWheel(rotationSpeed);
        }
    }

    /// <summary>
    /// Метод для вращения объекта колеса
    /// </summary>
    /// <param name="rotationAmount">Величина поворота</param>
    void RotateWheel(float rotationAmount)
    {
        // Вращаем колесо вокруг оси X. 
        // Используем Time.deltaTime, чтобы скорость была одинаковой при любом FPS.
        transform.Rotate(rotationAmount * Time.deltaTime, 0f, 0f);
    }
}
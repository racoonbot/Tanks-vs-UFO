using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClampPosition : MonoBehaviour
{
    [Header("Границы площадки (56x56 -> 28)")]
    public float maxX = 28f;
    public float minX = -28f;
    public float maxZ = 28f;
    public float minZ = -28f;

    [Header("Настройки")]
    public bool resetVelocityOnHit = true; // Сбрасывать ли инерцию при ударе о стену?
    public Rigidbody playerRigidbody; // Ссылка на тот дочерний объект, где лежит Rigidbody

    private void LateUpdate()
    {
        Vector3 pos = transform.position;

        // Проверяем, вышел ли игрок за границы
        if (pos.x > maxX || pos.x < minX || pos.z > maxZ || pos.z < minZ)
        {
            float clampedX = Mathf.Clamp(pos.x, minX, maxX);
            float clampedZ = Mathf.Clamp(pos.z, minZ, maxZ);

            transform.position = new Vector3(clampedX, pos.y, clampedZ);

            // Если есть ссылка на Rigidbody, обнуляем скорость по осям, 
            // чтобы AddForce не "накапливал" давление в стену
            if (resetVelocityOnHit && playerRigidbody != null)
            {
                Vector3 vel = playerRigidbody.velocity;
                if (pos.x >= maxX || pos.x <= minX) vel.x = 0;
                if (pos.z >= maxZ || pos.z <= minZ) vel.z = 0;
                playerRigidbody.velocity = vel;
            }
        }
    }
}

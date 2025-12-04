using UnityEngine;

public class Radar : MonoBehaviour
{
    public Transform enemy;
    public bool isDodging = false;
    private float dodgeSpeed = 10f;
    private float dodgeDistance = 10f;
    private Vector3 targetPosition; 
    private Vector3 initialPosition;

    private void Update()
    {
        transform.position = enemy.position;
        if (isDodging)
        {
            ChangePosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullets>())
        {

            initialPosition = enemy.position;
            Vector3 dodgeDirection = new Vector3(GetRandomDodgeDistance(), 0f, 0f); 
            targetPosition = initialPosition + dodgeDirection;

            isDodging = true;
        }
    }

    private void ChangePosition()
    {
        Debug.Log("ChangePosition()");
        enemy.position = Vector3.Lerp(enemy.position, targetPosition, Time.deltaTime * GetRandomDodgeSpeed());
        
        if (Vector3.Distance(enemy.position, targetPosition) < 0.1f)
        {
            isDodging = false; 
            enemy.position = targetPosition; 
        }
    }
    
    private float GetRandomDodgeSpeed()
    {
        return Random.Range(8f, 16f);
    }
    private float GetRandomDodgeDistance()
    {
        return Random.Range(-15f, 15f);
    }

    private float DodgeChance()
    {
        float distance = Vector3.Distance(enemy.position, targetPosition);
        float baseChance = Random.Range(0f, 100f); 

        // Увеличиваем шанс каждые 5 метров, но не более 50%
        if (distance >= 5 && distance < 10)
        {
            baseChance += 5; 
        }
        else if (distance >= 10 && distance < 15)
        {
            baseChance += 10; 
        }
        else if (distance >= 15 && distance < 20)
        {
            baseChance += 15; 
        }
        else if (distance >= 20 && distance < 25)
        {
            baseChance += 20; 
        }
        else if (distance >= 25)
        {
            baseChance += 25; 
        }
        baseChance = Mathf.Clamp(baseChance, 0f, 100f);
        return baseChance;
    }


}
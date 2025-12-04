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
}
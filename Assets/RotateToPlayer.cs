using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    public Transform player;
    public float RotationSpeed;
    
    void Start()
    {
        player = FindObjectOfType<Tank>().transform;
    }

    void Update()
    {
        if (player != null)
            ToPlayerRotation();
    }

    private void ToPlayerRotation()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0;

        if (dir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePositionEnemy : MonoBehaviour
{
    
    private void Update()
    {
        ConstYPosition();
    }

    private void ConstYPosition()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = 1f; 
        transform.position = currentPosition; 
    }
}

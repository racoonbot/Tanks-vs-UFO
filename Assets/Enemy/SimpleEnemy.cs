using System.Collections;
using UnityEngine;

public class SimpleEnemy : EnemyBase
{
    public ParticleSystem particles;
    public Transform SpawnPoint;

    private void OnDestroy()
    {
        ParticleSystem particleInstance = Instantiate(particles, SpawnPoint.position, Quaternion.identity);
        particleInstance.Play();
        
    }

   
}
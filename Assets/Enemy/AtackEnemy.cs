using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackEnemy : EnemyBase
{
    public ParticleSystem particles;
    public Transform SpawnPoint;

    public override string NickName => "Желтый";
    public override Color MyColor => Color.yellow;
    
    private void OnDestroy()
    {
        ParticleSystem particleInstance = Instantiate(particles, SpawnPoint.position, Quaternion.identity);
        particleInstance.Play();
     
    }

}

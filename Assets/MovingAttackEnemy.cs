using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAttackEnemy : EnemyBase
{
    public ParticleSystem particles;
    public Transform SpawnPoint;
    public override string NickName => "Красный";
    public override Color MyColor => Color.red;
    private void OnDestroy()
    {
        ParticleSystem particleInstance = Instantiate(particles, SpawnPoint.position, Quaternion.identity);
        particleInstance.Play();
  
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MovingAttackEnemy : EnemyBase
{
    public ParticleSystem particles;
    public Transform SpawnPoint;
    public override string NickName => "Red";
    public override Color MyColor => Color.red;

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded && particles != null)
        {
            ParticleSystem particleInstance = Instantiate(particles, SpawnPoint.position, Quaternion.identity);
            particleInstance.Play();
            Destroy(particleInstance.gameObject, particleInstance.main.duration + particleInstance.main.startLifetime.constantMax);
        }
    }
}
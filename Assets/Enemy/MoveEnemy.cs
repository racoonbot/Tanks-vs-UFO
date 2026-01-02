using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : EnemyBase
{
    public ParticleSystem particles;
    public Transform SpawnPoint;
    public override string NickName => "Желтый";
    public override Color MyColor => Color.yellow;
    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            ParticleSystem particleInstance = Instantiate(particles, SpawnPoint.position, Quaternion.identity);
            particleInstance.Play();
            Destroy(particleInstance.gameObject, particleInstance.main.duration + particleInstance.main.startLifetime.constantMax);
        }

    }
}

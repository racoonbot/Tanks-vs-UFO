using System.Collections;
using UnityEngine;

public class SimpleEnemy : EnemyBase
{
    public ParticleSystem particles;
    public Transform SpawnPoint;
    public override string NickName => "Зеленый";
    public override Color MyColor => Color.green;
    

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded && particles != null)
        {
            ParticleSystem particleInstance = Instantiate(particles, SpawnPoint.position, Quaternion.identity);
            particleInstance.Play();
            Destroy(particleInstance.gameObject,
                particleInstance.main.duration + particleInstance.main.startLifetime.constantMax);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class TankHealth : MonoBehaviour
{
    public GameObject player;
    public float health;
    private TankAttributes attributes;
    public AudioSource audioSource;

    private float lastDamageTime;
    public float damageCooldown = 0.2f;

    [Header("Накопительный бонус")]
    public float timeForBonus = 3;
    public int bonusLevel = 0;
    public int maxBonusLevel;

    public Action OnDeathPlayer;

    // Для окрашивания при уроне
    public float hurtFlashDuration = 0.1f;
    public Color hurtColor = Color.red;

    // Храним рендереры и их исходные цвета
    private Renderer[] renderers;
    private List<Material[]> originalMaterials = new List<Material[]>();

    private void Start()
    {
        attributes = FindObjectOfType<TankAttributes>();
        if (attributes != null)
            health = attributes.maxHealth;

        lastDamageTime = Time.time;

        // Собираем все Renderer'ы у танка (включая дочерние) и сохраняем их материалы
        renderers = GetComponentsInChildren<Renderer>();
        originalMaterials.Clear();
        foreach (var rend in renderers)
        {
            // Сохраняем копии материалов чтобы потом восстанавливать
            Material[] matsCopy = new Material[rend.materials.Length];
            for (int i = 0; i < rend.materials.Length; i++)
            {
                matsCopy[i] = new Material(rend.materials[i]);
            }
            originalMaterials.Add(matsCopy);
        }
    }

    private void Update()
    {
        CalculateBonusLevel();
    }

    private void CalculateBonusLevel()
    {
        float timeWithoutDamage = Time.time - lastDamageTime;
        if (timeWithoutDamage < timeForBonus)
        {
            bonusLevel = 0;
        }
        else
        {
            bonusLevel = (int)(timeWithoutDamage / timeForBonus);
            if (bonusLevel > maxBonusLevel)
                bonusLevel = maxBonusLevel;
        }
    }

    public void TakeDamage()
    {
        if (Time.time > lastDamageTime + damageCooldown)
        {
            audioSource.Play();
            health--;
            lastDamageTime = Time.time;
            StopCoroutine("FlashHurt"); // безопасно остановить предыдущую (если была)
            StartCoroutine(FlashHurt());

            if (health <= 0)
            {
                Die();
            }
        }
    }

    public float GetBonusProgress()
    {
        float timeToMaxLevel = timeForBonus * maxBonusLevel;
        float timeSinceDamage = Time.time - lastDamageTime;
        float totalProgress = timeSinceDamage / timeToMaxLevel;
        return Mathf.Clamp01(totalProgress);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bullets bullet) || other.TryGetComponent(out EnemyBase enemy))
        {
            TakeDamage();
        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
    }

    public void Die()
    {
        OnDeathPlayer?.Invoke();
        YG2.MetricaSend("PlayerDead");
        if (MusicPlayer.instance != null) MusicPlayer.instance.StopAllMusic();
        if (player != null) Destroy(player.gameObject);
        else Destroy(gameObject);
    }

    private IEnumerator FlashHurt()
    {
        if (renderers == null || renderers.Length == 0)
            yield break;
        for (int r = 0; r < renderers.Length; r++)
        {
            var rend = renderers[r];
            Material[] flashMats = new Material[rend.materials.Length];
            for (int i = 0; i < flashMats.Length; i++)
            {
                Material m = new Material(rend.materials[i]);
                if (m.HasProperty("_Color"))
                    m.color = hurtColor;
                else if (m.HasProperty("_BaseColor")) // URP/HDRP
                    m.SetColor("_BaseColor", hurtColor);
                flashMats[i] = m;
            }
            rend.materials = flashMats;
        }

        yield return new WaitForSeconds(hurtFlashDuration);
        
        for (int r = 0; r < renderers.Length; r++)
        {
            var rend = renderers[r];
            if (r < originalMaterials.Count)
            {
                Material[] matsToRestore = new Material[originalMaterials[r].Length];
                for (int i = 0; i < matsToRestore.Length; i++)
                {
                    matsToRestore[i] = new Material(originalMaterials[r][i]);
                }
                rend.materials = matsToRestore;
            }
        }
    }
}

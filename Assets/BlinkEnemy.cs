using System.Collections;
using UnityEngine;

public class BlinkEnemy : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Material[] materials;
    private Color[] initialColors; // Массив для хранения начальных цветов

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        materials = meshRenderer.materials; // Получаем все материалы

        // Сохраняем начальные цвета
        initialColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            initialColors[i] = materials[i].GetColor("_Color");
        }
    }

    public void StartBlinking()
    {
        StartCoroutine(BlinkEffect());
    }

    public IEnumerator BlinkEffect()
    {

        for (float t = 0f; t < 1; t += Time.deltaTime)
        {
            Color blinkColor = new Color(Mathf.Sin(t * 40), 0f, 0f, t);


            foreach (Material material in materials)
            {
                material.SetColor("_EmissionColor", blinkColor);
            }

            yield return null;
        }


        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetColor("_Color", initialColors[i]);
        }

        foreach (Material material in materials)
        {
            material.SetColor("_EmissionColor", Color.black);
        }
    }

    private void Shake()
    {
        StartCoroutine(ShakeEffect());
    }

    private IEnumerator ShakeEffect()
    {
        Vector3 originalPosition = transform.localPosition; 
        Vector3 randomDirection = Random.insideUnitSphere * 0.2f; 

        float duration = 0.2f; 
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.localPosition = originalPosition + randomDirection * Mathf.Sin(elapsed * Mathf.PI * 10); 
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}

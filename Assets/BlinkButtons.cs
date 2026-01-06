using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkButtons : MonoBehaviour
{
    private TankAttributes _tankAttributes;

    [Header("Настройки")]
    // 1. ЭТО ВАЖНО: У каждой кнопки будет свой тип, который ты выберешь в инспекторе
    public StatType myStatType; 
    
    public Image buttonImage;
    private Color _originalColor;

    private void Awake()
    {
        _tankAttributes = GetComponentInParent<TankAttributes>();
        if (buttonImage == null) buttonImage = GetComponentInChildren<Image>(); 
        if (buttonImage != null) _originalColor = buttonImage.color;
    }

    private void OnEnable()
    {
        if (_tankAttributes == null) _tankAttributes = FindObjectOfType<TankAttributes>();
        if (_tankAttributes != null)
        {
            _tankAttributes.OnMaximumLevelReached += Blink;
        }
    }

    private void OnDisable()
    {
        if (_tankAttributes != null)
            _tankAttributes.OnMaximumLevelReached -= Blink;
    }

    // 2. Принимаем тип характеристики (incomingStat), который прислал танк
    private void Blink(StatType incomingStat)
    {  
        // 3. ПРОВЕРКА: Если танк прислал "Speed", а я кнопка "Speed" — я мигаю.
        // Если танк прислал "Health", а я "Speed" — я молчу.
        if (incomingStat == myStatType) 
        {
            StopAllCoroutines(); 
            StartCoroutine(BlinkCoroutine());
        }
    }

    private IEnumerator BlinkCoroutine()
    {
        // Код корутины остается прежним...
        if (buttonImage == null) yield break;
        float duration = 0.5f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            buttonImage.color = Color.Lerp(_originalColor, Color.black, elapsed / duration);
            yield return null;
        }
        buttonImage.color = _originalColor;
    }
}
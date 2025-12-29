using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkButtons : MonoBehaviour
{
    private TankAttributes _tankAttributes;
    
    private Image _buttonImage;
    private Color _originalColor;
    void Awake()
    {
        _tankAttributes = FindObjectOfType<TankAttributes>();
        _buttonImage = GetComponent<Image>();
        if (_buttonImage != null)
        {
            _originalColor = _buttonImage.color;
        }
    }

    private void OnEnable()
    {
        if (_tankAttributes == null) 
            _tankAttributes = FindObjectOfType<TankAttributes>();

        if (_tankAttributes != null)
        {
            _tankAttributes.OnMaximumLevelReached += Blink;
        }
    }

    private void OnDisable()
    {
        _tankAttributes.OnMaximumLevelReached -= Blink;
    }

    private void Blink()
    {  Debug.Log("_Blink;");
        StopAllCoroutines(); 
        StartCoroutine(BlinkCoroutine());
    }
    private IEnumerator BlinkCoroutine()
    {
        Debug.Log("Blinking");
        if (_buttonImage == null) yield break;

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _buttonImage.color = Color.Lerp(_originalColor, Color.black, elapsed / duration);
            yield return null;
        }
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _buttonImage.color = Color.Lerp(Color.black, _originalColor, elapsed / duration);
            yield return null;
        }

        _buttonImage.color = _originalColor;
    }
}

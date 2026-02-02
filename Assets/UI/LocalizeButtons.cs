using UnityEngine;
using UnityEngine.UI;
using YG;

public class LocalizeButtons : MonoBehaviour
{
    [Header("Картинки для языков")]
    public Sprite ruSprite;
    public Sprite enSprite;

    private Image imageComponent;
    public bool isLanguageStartMenu;

    private void Awake()
    {
        imageComponent = GetComponent<Image>();
    }

    private void OnEnable()
    {
        YG2.onCorrectLang += ApplyLanguage;
        if (YG2.isSDKEnabled)
        {
            ApplyLanguage(YG2.lang);
        }
    }

    private void OnDisable()
    {
        YG2.onCorrectLang -= ApplyLanguage;
    }

    private void ApplyLanguage(string lang)
    {
        if (imageComponent == null) return;
        if (string.IsNullOrEmpty(lang)) lang = "en";

        lang = lang.ToLowerInvariant();

        if (lang == "ru")
        {
            if (ruSprite != null && imageComponent.sprite != ruSprite)
                imageComponent.sprite = ruSprite;
        }
        else // default -> english
        {
            if (enSprite != null && imageComponent.sprite != enSprite)
                imageComponent.sprite = enSprite;
        }
    }
}
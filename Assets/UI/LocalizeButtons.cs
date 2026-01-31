using System;
using Unity.VisualScripting;
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

    private void Update()
    {
        if (isLanguageStartMenu)
        {
            OnEnable();
        }
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
        if (lang == "ru" && imageComponent.sprite != ruSprite)
        {
            if (ruSprite != null) imageComponent.sprite = ruSprite;
        }
        else if(imageComponent.sprite != enSprite)
        {
            if (enSprite != null) imageComponent.sprite = enSprite;
        }
    }
}
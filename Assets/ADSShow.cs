using UnityEngine;
using YG;

public class ADSShow : MonoBehaviour
{
    private LevelManager levelManager;
    private bool adAlreadyShown = false; 

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        if (levelManager.levelIncreased)
        {
            if (!adAlreadyShown) 
            {
                ShowAds();  
                adAlreadyShown = true;
            }
        }
        else 
        {
            adAlreadyShown = false; 
        }
    }

    public void ShowAds()
    {
        Debug.Log("Показываем рекламу");
        YG2.InterstitialAdvShow();
    }
}
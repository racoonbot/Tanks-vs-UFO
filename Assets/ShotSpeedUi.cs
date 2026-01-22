using UnityEngine;
using UnityEngine.UI;

public class ShotSpeedUi : MonoBehaviour
{
    private Image barImage;
    public TankHealth tankHealth; 

    void Start()
    {
        barImage = GetComponent<Image>();
        
        if (tankHealth == null)
        {
            tankHealth = FindObjectOfType<TankHealth>();
        }
    }

    void Update()
    {
        if (tankHealth != null && barImage != null)
        {
            barImage.fillAmount = tankHealth.GetBonusProgress();
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class AudioUIConnector : MonoBehaviour
{
    [Header("Перетащите сюда слайдеры из текущей сцены")]
    public Slider sceneMusicSlider;
    public Slider sceneSoundsSlider;

    private void OnEnable()
    {
        
        if (AudioSettings.instance != null)
        {
            AudioSettings.instance.RegisterSliders(sceneMusicSlider, sceneSoundsSlider);
        }
     }
}
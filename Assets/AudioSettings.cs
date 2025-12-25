using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Настройки")]
    public AudioMixer masterMixer; // Сюда перетащить Mixer
    public Slider slider;          // Сюда перетащить Слайдер (только в Меню)

    private void Start()
    {
        // 1. Загружаем сохраненное значение
        float savedValue = PlayerPrefs.GetFloat("MasterVolume", 1f);

        // 2. Сначала выставляем звук в Микшере (чтобы сразу было тихо)
        SetMixerVolume(savedValue);

        // 3. Если мы привязали слайдер (мы в Меню), настраиваем его визуал
        if (slider != null)
        {
            // Двигаем ручку на нужную позицию БЕЗ вызова звука
            slider.SetValueWithoutNotify(savedValue);

            // Теперь подписываемся на изменения игрока
            slider.onValueChanged.RemoveAllListeners();
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    public void OnSliderValueChanged(float value)
    {
        SetMixerVolume(value);
        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();
    }

    private void SetMixerVolume(float sliderValue)
    {
        float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20;
        masterMixer.SetFloat("MasterVolume", volume);
    }
}
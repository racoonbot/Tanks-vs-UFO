using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider slider;
    public AudioMixer masterMixer;

    private void Start()
    {
        Debug.Log("Загруженное значение из PlayerPrefs: " + PlayerPrefs.GetFloat("MasterVolume"));
        // Загрузка значения из PlayerPrefs
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f); // 1f — значение по умолчанию
        slider.value = masterVolume; // Установка значения слайдера

        // Установка громкости в AudioMixer
        float volume = Mathf.Log10(masterVolume) * 20;
        masterMixer.SetFloat("MasterVolume", volume);
    }

    public void SetVolume(float sliderValue)
    {
        // Преобразование значения слайдера в значение громкости
        float volume = Mathf.Log10(sliderValue) * 20;
        masterMixer.SetFloat("MasterVolume", volume);

        // Сохранение значения в PlayerPrefs
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
        PlayerPrefs.Save(); // Явно сохранить изменения
    }
}
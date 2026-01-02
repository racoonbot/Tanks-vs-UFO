using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("--- МУЗЫКА (Слайдер 1) ---")]
    public AudioMixer musicMixer;   // Сюда перетащить MainMixer (или тот, где музыка)
    public Slider musicSlider;      // Сюда перетащить Слайдер музыки
    private string musicParam = "MasterVolume"; // Имя Exposed параметра музыки

    [Header("--- ЗВУКИ (Слайдер 2) ---")]
    public AudioMixer soundsMixer;  // Сюда перетащить микшер Sounds
    public Slider soundsSlider;     // Сюда перетащить Слайдер звуков
    private string soundsParam = "SoundsMaster"; // Имя Exposed параметра звуков (который мы создавали)

    private void Start()
    {
        // === НАСТРОЙКА МУЗЫКИ ===
        float savedMusic = PlayerPrefs.GetFloat("MasterVolume", 1f);
        SetMusicVolume(savedMusic); // Применяем громкость

        if (musicSlider != null)
        {
            musicSlider.SetValueWithoutNotify(savedMusic); // Ставим ползунок
            musicSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.AddListener(OnMusicSliderChanged); // Подписываемся
        }

        // === НАСТРОЙКА ЗВУКОВ ===
        float savedSounds = PlayerPrefs.GetFloat("SoundsVolume", 1f);
        SetSoundsVolume(savedSounds); // Применяем громкость

        if (soundsSlider != null)
        {
            soundsSlider.SetValueWithoutNotify(savedSounds); // Ставим ползунок
            soundsSlider.onValueChanged.RemoveAllListeners();
            soundsSlider.onValueChanged.AddListener(OnSoundsSliderChanged); // Подписываемся
        }
    }

    // --- МЕТОДЫ ДЛЯ МУЗЫКИ ---
    public void OnMusicSliderChanged(float value)
    {
        SetMusicVolume(value);
        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();
    }

    private void SetMusicVolume(float sliderValue)
    {
        float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20;
        musicMixer.SetFloat(musicParam, volume);
    }

    // --- МЕТОДЫ ДЛЯ ЗВУКОВ ---
    public void OnSoundsSliderChanged(float value)
    {
        SetSoundsVolume(value);
        PlayerPrefs.SetFloat("SoundsVolume", value);
        PlayerPrefs.Save();
    }

    private void SetSoundsVolume(float sliderValue)
    {
        float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20;
        // Здесь используем soundsMixer и параметр SoundsMaster
        soundsMixer.SetFloat(soundsParam, volume);
    }
}
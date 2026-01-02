using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public static AudioSettings instance;

    [Header("Микшеры (Назначьте их один раз в префабе)")]
    public AudioMixer musicMixer;
    public AudioMixer soundsMixer;

    // Слайдеры делаем приватными или просто скрываем, так как они будут меняться
    private Slider musicSlider;
    private Slider soundsSlider;

    private string musicParam = "MasterVolume";
    private string soundsParam = "SoundsMaster";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // При старте (в главном меню) применим настройки, даже если слайдеров пока нет
        ApplyVolumeSettings();
    }

    // === ЭТОТ МЕТОД ВЫЗЫВАЕТ "СВЯЗНОЙ" ИЗ НОВОЙ СЦЕНЫ ===
    public void RegisterSliders(Slider newMusicSlider, Slider newSoundSlider)
    {
        musicSlider = newMusicSlider;
        soundsSlider = newSoundSlider;

        // Как только получили новые слайдеры, сразу настраиваем их
        InitializeSliders();
    }

    private void InitializeSliders()
    {
        // 1. Настройка слайдера МУЗЫКИ
        if (musicSlider != null)
        {
            float savedMusic = PlayerPrefs.GetFloat("MasterVolume", 1f);
            musicSlider.SetValueWithoutNotify(savedMusic);
            musicSlider.onValueChanged.RemoveAllListeners(); // Удаляем старые связи
            musicSlider.onValueChanged.AddListener(OnMusicSliderChanged); // Добавляем новые
        }

        // 2. Настройка слайдера ЗВУКОВ
        if (soundsSlider != null)
        {
            float savedSounds = PlayerPrefs.GetFloat("SoundsVolume", 1f);
            soundsSlider.SetValueWithoutNotify(savedSounds);
            soundsSlider.onValueChanged.RemoveAllListeners();
            soundsSlider.onValueChanged.AddListener(OnSoundsSliderChanged);
        }
    }

    private void ApplyVolumeSettings()
    {
        float savedMusic = PlayerPrefs.GetFloat("MasterVolume", 1f);
        SetMusicVolume(savedMusic);

        float savedSounds = PlayerPrefs.GetFloat("SoundsVolume", 1f);
        SetSoundsVolume(savedSounds);
    }

    // --- LOGIC ---

    public void OnMusicSliderChanged(float value)
    {
        SetMusicVolume(value);
        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();
    }

    public void OnSoundsSliderChanged(float value)
    {
        SetSoundsVolume(value);
        PlayerPrefs.SetFloat("SoundsVolume", value);
        PlayerPrefs.Save();
    }

    private void SetMusicVolume(float sliderValue)
    {
        if (musicMixer != null)
        {
            float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20;
            musicMixer.SetFloat(musicParam, volume);
        }
    }

    private void SetSoundsVolume(float sliderValue)
    {
        if (soundsMixer != null)
        {
            float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20;
            soundsMixer.SetFloat(soundsParam, volume);
        }
    }
}
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public static AudioSettings instance;

    [Header("Микшеры (Назначьте их один раз в префабе)")]
    public AudioMixer musicMixer;
    public AudioMixer soundsMixer;

    // Слайдеры приватные, так как они подтягиваются через RegisterSliders
    private Slider musicSlider;
    private Slider soundsSlider;

    // Имена параметров в твоих Audio Mixer (Exposed Parameters)
    private string musicParam = "MasterVolume";
    private string soundsParam = "SoundsMaster";

    private void Awake()
    {
        // Паттерн Singleton: объект живет между сценами
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
        // При старте применяем сохраненные значения из PlayerPrefs
        ApplyVolumeSettings();
    }

    // === ЭТОТ МЕТОД ВЫЗЫВАЕТ "СВЯЗНОЙ" (AudioUIConnector) ИЗ НОВОЙ СЦЕНЫ ===
    public void RegisterSliders(Slider newMusicSlider, Slider newSoundSlider)
    {
        musicSlider = newMusicSlider;
        soundsSlider = newSoundSlider;

        // Настраиваем полученные слайдеры: ставим им значения и подписываем на события
        InitializeSliders();
    }

    private void InitializeSliders()
    {
        // 1. Настройка слайдера МУЗЫКИ
        if (musicSlider != null)
        {
            float savedMusic = PlayerPrefs.GetFloat("MasterVolume", 1f);
            musicSlider.SetValueWithoutNotify(savedMusic);
            musicSlider.onValueChanged.RemoveAllListeners(); 
            musicSlider.onValueChanged.AddListener(OnMusicSliderChanged); 
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

    public void ApplyVolumeSettings()
    {
        float savedMusic = PlayerPrefs.GetFloat("MasterVolume", 1f);
        SetMusicVolume(savedMusic);

        float savedSounds = PlayerPrefs.GetFloat("SoundsVolume", 1f);
        SetSoundsVolume(savedSounds);
    }

    // --- ЛОГИКА ИЗМЕНЕНИЯ (Вызывается слайдерами) ---

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

    // Перевод значения слайдера в децибелы для микшера
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
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer masterMixer;
    private Slider slider; // Убрали public, будем искать его сами

    public static AudioSettings Instance { get; private set; }

    private void Awake()
    {
        // Логика Singleton (Одиночки)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        // Подписываемся на событие загрузки сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Отписываемся, чтобы не было ошибок
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Этот метод вызывается каждый раз, когда загружается ЛЮБАЯ сцена
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndSetupSlider();
    }

    private void FindAndSetupSlider()
    {
        GameObject sliderObj = GameObject.Find("VolumeSlider");

        if (sliderObj != null)
        {
            slider = sliderObj.GetComponent<Slider>();

            // 1. Сначала достаем значение из памяти
            float savedValue = PlayerPrefs.GetFloat("MasterVolume", 1f);
        
            // 2. ВАЖНО: Используем SetValueWithoutNotify. 
            // Это поставит ползунок на место, но НЕ вызовет метод SetVolume.
            // Так мы защитим память от случайного сброса в "1" при загрузке.
            slider.SetValueWithoutNotify(savedValue);

            // 3. Теперь подписываемся на будущие изменения игрока
            slider.onValueChanged.RemoveAllListeners();
            slider.onValueChanged.AddListener(SetVolume);
        
            // 4. Обязательно применяем громкость к микшеру
            UpdateAudioMixerVolume(savedValue);

            Debug.Log("✅ Слайдер настроен 'тихо'. Значение из памяти: " + savedValue);
        }
    }

    public void SetVolume(float sliderValue)
    {
        UpdateAudioMixerVolume(sliderValue);
        Debug.Log("💾 Сохраняю в PlayerPrefs значение: " + sliderValue);
        // Сохраняем значение
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
        PlayerPrefs.Save();
    }

    private void UpdateAudioMixerVolume(float sliderValue)
    {
        // Ограничиваем значение, чтобы Log10 не выдал ошибку
        float clampedValue = Mathf.Clamp(sliderValue, 0.0001f, 1f);
        float dbVolume = Mathf.Log10(clampedValue) * 20;
        
        masterMixer.SetFloat("MasterVolume", dbVolume);
    }
}
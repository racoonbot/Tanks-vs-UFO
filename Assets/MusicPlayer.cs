using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    public AudioSource[] audioSources;
    // Добавьте ссылку на микшер в инспекторе, если она еще не там
    public AudioMixer mainMixer; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            Debug.Log("<color=cyan>[MusicPlayer]</color> Инициализирован.");
            PerformSystemCheck();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"<color=cyan>[MusicPlayer]</color> Загружена сцена: <b>{scene.name}</b>");
        
        if (scene.name == "StartScene")
        {
            PlayMusicSafe(0);
        }
        else if (scene.name == "SampleScene")
        {
            PlayMusicSafe(1);
        }
    }

    public void PlayMusicSafe(int index)
    {
        if (audioSources == null || audioSources.Length == 0) return;

        if (index < 0 || index >= audioSources.Length) return;

        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i] == null) continue;

            if (i == index)
            {
                if (!audioSources[i].isPlaying)
                {
                    audioSources[i].Play();
                    Debug.Log($"<color=green>[MusicPlayer]</color> Начинаю играть аудио индекс {i}");
                }
            }
            else
            {
                if (audioSources[i].isPlaying) audioSources[i].Stop();
            }
        }
    }

    public void StopAllMusic()
    {
        if (audioSources == null) return;
        foreach (var source in audioSources)
        {
            if (source != null) source.Stop();
        }
    }

    private void PerformSystemCheck()
    {
        // ПРИНУДИТЕЛЬНЫЙ СБРОС ГРОМКОСТИ МИКШЕРА
        if (mainMixer != null)
        {
            // Пытаемся выставить громкость Master на 0 (максимум)
            // Имя "MasterVolume" должно быть выставлено в Exposed Parameters микшера
            mainMixer.SetFloat("MasterVolume", 0f); 
            Debug.Log("<color=white>[Микшер]</color> Попытка сброса громкости MasterVolume на 0dB.");
        }

        if (AudioListener.pause) Debug.LogError("AudioListener на ПАУЗЕ!");
        if (AudioListener.volume <= 0.1f) Debug.LogWarning("Глобальная громкость почти на нуле!");
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    public AudioSource[] audioSources;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
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
        if (scene.name == "StartScene")
        {

            PlayMusicSafe(0);
        }
        else if (scene.name == "SampleScene")
        {

            PlayMusicSafe(1);
        }
    }

    private void PlayMusicSafe(int index)
    {
        if (audioSources == null || index < 0 || index >= audioSources.Length) return;
        // Остановить все и запустить нужный
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i] == null) continue;
            if (i == index)
            {
                if (!audioSources[i].isPlaying) audioSources[i].Play();
            }
            else
            {
                if (audioSources[i].isPlaying) audioSources[i].Stop();
            }
        }
    }
}
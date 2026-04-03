using UnityEngine;
using UnityEditor;

public class AudioFixer : Editor
{
    [MenuItem("Tools/Force Repair Audio for WebGL")]
    public static void ForceRepair()
    {
        // Получаем все выделенные файлы в окне Project
        Object[] selectedObjects = Selection.objects;

        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("Сначала выдели аудиофайлы (Menu и Game) в окне Project!");
            return;
        }

        foreach (Object obj in selectedObjects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            AudioImporter importer = AssetImporter.GetAtPath(path) as AudioImporter;

            if (importer != null)
            {
                Debug.Log($"<color=orange>Обработка:</color> {path}");

                // 1. Создаем настройки специально для WebGL
                AudioImporterSampleSettings webGLSettings = new AudioImporterSampleSettings();
                
                // 2. Устанавливаем формат Vorbis (он стабильнее AAC в Unity)
                webGLSettings.compressionFormat = AudioCompressionFormat.Vorbis;
                webGLSettings.quality = 0.5f; // Качество 50%
                webGLSettings.sampleRateSetting = AudioSampleRateSetting.OptimizeSampleRate;
                webGLSettings.loadType = AudioClipLoadType.CompressedInMemory;

                // 3. Применяем настройки принудительно
                importer.SetOverrideSampleSettings("WebGL", webGLSettings);
                importer.forceToMono = true; // Для WebGL лучше моно, если это не критично

                // 4. Перезагружаем файл в базу данных
                importer.SaveAndReimport();
                
                Debug.Log("<color=green>Готово!</color> Файл перенастроен на Vorbis. Проверь инспектор.");
            }
        }
    }
}
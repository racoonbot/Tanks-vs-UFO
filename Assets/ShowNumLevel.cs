using TMPro;
using UnityEngine;
using YG;

public class ShowNumLevel : MonoBehaviour
{
    public TextMeshProUGUI text;
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager == null)
            Debug.Log("levelManager == null");
        UpdateText();
    }

    public void UpdateText()
    {
        if (YG2.lang == "en")
        {
            text.text = $"Level  {levelManager.level}";
        }
        else if (YG2.lang == "ru")
        {
            text.text = $"Уровень  {levelManager.level}";
        }
        else
        {
            text.text = $"Level  {levelManager.level}";
        }
    }
}
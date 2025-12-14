using TMPro;
using UnityEngine;

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
        text.text = $"Уровень {levelManager.level}"  ;
    }
}
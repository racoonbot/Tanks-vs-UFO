using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowControl : MonoBehaviour
{
    public GameObject controlHelpUI;
    
    IEnumerator Start()
    {
        controlHelpUI.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        ShowHelp();
    }

    public void ShowHelp()
    {
        Time.timeScale = 0;
        controlHelpUI.SetActive(true);
       UnlockCursor();
    }

    public void HideHelp()
    {
        Time.timeScale = 1;
        controlHelpUI.SetActive(false);
       // LockCursor();
    }

    private void UnlockCursor() // Разблокируем мышь
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockCursor() // Блокируем мышь
    {
        // ВНИМАНИЕ: Я исправил этот метод. 
        // Раньше тут было None и visible = true, как в UnlockCursor.
        Cursor.lockState = CursorLockMode.Locked; // Теперь курсор заблокирован в центре
        Cursor.visible = false;                   // И скрыт
    }
}
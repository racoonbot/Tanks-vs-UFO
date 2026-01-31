using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

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
        LevelManager.UnlockCursor(this);
    }

    public void HideHelp()
    {
        LevelManager.LockCursor(this);
        Time.timeScale = 1;
        controlHelpUI.SetActive(false);
        
    }
}
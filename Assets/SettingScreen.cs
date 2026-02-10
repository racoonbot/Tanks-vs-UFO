using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingScreen : MonoBehaviour
{
    public GameObject settingScreen;

    private void Start()
    {
       // settingScreen.SetActive(false);
    }

    public void ShowSettings()
    {
        settingScreen.SetActive(true);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAttributerUi : MonoBehaviour
{
    private void Start()
    {
        HideUi();
    }

    public void HideUi()
    {
        gameObject.SetActive(false);
    }

    public void ShowUi()
    {
        gameObject.SetActive(true);
    }
}

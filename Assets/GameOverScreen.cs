using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    private TankHealth _tankHealth;
    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.gameObject.SetActive(false);
        _tankHealth = FindObjectOfType<TankHealth>();
        _tankHealth.OnDeathPlayer += ShowGameOverScreen;
    }

    private void OnDisable()
    {
        _tankHealth.OnDeathPlayer -= ShowGameOverScreen;
    }


    private void ShowGameOverScreen()
    {
        gameOverScreen.gameObject.SetActive(true);
    }
}

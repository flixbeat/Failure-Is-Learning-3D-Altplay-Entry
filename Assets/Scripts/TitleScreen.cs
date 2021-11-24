using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private LevelSelect levelSelect;

    public void Play()
    {
        GameManager.startGame.Invoke();
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void ShowLevelSelect()
    {
        main.SetActive(false);
        levelSelect.gameObject.SetActive(true);
    }

    public void ShowMain()
    {
        main.SetActive(true);
        levelSelect.gameObject.SetActive(false);
    }
}

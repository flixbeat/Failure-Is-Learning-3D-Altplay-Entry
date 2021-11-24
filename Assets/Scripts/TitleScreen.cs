using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject levelSelect;

    public void Play()
    {
        GameManager.startGame.Invoke();
        gameObject.SetActive(false);
    }

    public void ShowLevelSelect()
    {
        main.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void ShowMain()
    {
        main.SetActive(true);
        levelSelect.SetActive(false);
    }
}

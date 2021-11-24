using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{

    [SerializeField] private Transform stuckPanel;
    [SerializeField] private MenuBar menuBar;
    [SerializeField] private GameOver gameOver;
    [SerializeField] private TitleScreen titleScreen;
    
    public void ShowStuckPanel()
    {
        stuckPanel.gameObject.SetActive(true);
    }

    public void HideStuckPanel()
    {
        stuckPanel.gameObject.SetActive(false);
    }
    
    public void ShowMenuBar()
    {
        menuBar.gameObject.SetActive(true);
    }

    public void HideMenuBar()
    {
        menuBar.gameObject.SetActive(false);
    }

    public void RestoreLife()
    {
        menuBar.RestoreLife();
    }

    public void DeductLife()
    {
        menuBar.DeductLife();
    }

    public void ShowGameOver()
    {
        gameOver.gameObject.SetActive(true);
        HideMenuBar();
    }

    public void ShowTitleScreen()
    {
        titleScreen.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);
    }
}

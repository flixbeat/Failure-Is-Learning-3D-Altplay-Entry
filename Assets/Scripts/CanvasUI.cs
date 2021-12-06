using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{
    [SerializeField] private Transform stuckPanel;
    [SerializeField] private MenuBar menuBar;
    [SerializeField] private GameOver gameOver;
    [SerializeField] private GameOver finish;
    [SerializeField] private TitleScreen titleScreen;
    [SerializeField] private PauseGame pauseGame;
    
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
        GameManager.toTitleScreen.Invoke();
    }
    
    public void HideTitleScreen()
    {
        titleScreen.gameObject.SetActive(false);
    }

    public void ShowFinishScreen()
    {
        finish.gameObject.SetActive(true);
        HideMenuBar();
    }
    
    public void ShowPauseScreen()
    {
        pauseGame.Show();
    }
}

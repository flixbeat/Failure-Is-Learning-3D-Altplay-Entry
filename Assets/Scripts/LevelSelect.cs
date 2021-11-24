using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private Transform levelContainer;
    
    private void OnEnable()
    {
        DisableButtons();
        LoadUnlockedLevels();
    }

    private void DisableButtons()
    {
        foreach (Transform levelButton in levelContainer)
            levelButton.GetComponent<Button>().interactable = false;
    }

    private void LoadUnlockedLevels()
    {
        int levels = PlayerPrefs.GetInt("level") + 1;
        for (int i = 0; i < levels; i++)
            levelContainer.GetChild(i).GetComponent<Button>().interactable = true;
    }

    public void LoadLevel(int level)
    {
        GameManager.loadGame.Invoke(level);
    }
}

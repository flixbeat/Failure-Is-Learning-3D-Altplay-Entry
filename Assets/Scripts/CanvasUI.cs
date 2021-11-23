using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{

    [SerializeField] private Transform stuckPanel;
    

    public void ShowStuckPanel()
    {
        stuckPanel.gameObject.SetActive(true);
    }

    public void HideStuckPanel()
    {
        stuckPanel.gameObject.SetActive(false);
    }
}

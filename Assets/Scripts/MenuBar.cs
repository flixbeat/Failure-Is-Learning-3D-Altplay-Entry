using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBar : MonoBehaviour
{
    private const int maxLife = 3;
    public static int lifeCount;
    
    [SerializeField] private Transform lifeContainer;
    [SerializeField] private Transform life;

    private void OnEnable()
    {
        lifeCount = 0;
    }
    
    private void OnDisable()
    {
        lifeCount = -1;
    }

    public void RestoreLife()
    {
        lifeCount = 0;
        
        foreach (Transform life in lifeContainer)
            Destroy(life.gameObject);

        for (int i = 0; i < maxLife; i++)
        {
            Instantiate(life, lifeContainer);
            lifeCount += 1;
        }
    }

    public void DeductLife()
    {
        if (lifeContainer.childCount > 0)
        {
            Transform life = lifeContainer.GetChild(0);
            Destroy(life.gameObject);
            lifeCount -= 1;
        }
        else
            GameManager.endGame.Invoke();
    }
}

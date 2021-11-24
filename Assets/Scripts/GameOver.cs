using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private const float delayShowSec = 0;
    
    [SerializeField] private Transform btnPlayAgain;
    [SerializeField] private Transform btnQuit;

    private void OnEnable()
    {
        StartCoroutine(DelayButtons());
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    // delay the display of buttons, can be configured
    IEnumerator DelayButtons()
    {
        btnPlayAgain.gameObject.SetActive(false);
        btnQuit.gameObject.SetActive(false);
            
        yield return new WaitForSeconds(delayShowSec);
            
        btnPlayAgain.gameObject.SetActive(true);
        btnQuit.gameObject.SetActive(true);
    }
}

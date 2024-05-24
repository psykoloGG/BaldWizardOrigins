using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public GameObject WinMenuPanel;
    public GameObject NextLevelButton;

    private void Awake()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 1)
        {
            NextLevelButton.SetActive(false);
        }
        else
        {
            NextLevelButton.SetActive(true);
        }
    }

    public void ShowWinMenu()
    {
        WinMenuPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }
    
    public void NextLevel()
    {
        Time.timeScale = 1.0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }
}

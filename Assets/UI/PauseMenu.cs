using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public TMP_Text LevelText;
    public static bool GameIsPaused = false;
    public GameObject PauseMenuPanel;
    
    private void Awake()
    {
        LevelText.text = "Level: " + SceneManager.GetActiveScene().buildIndex;
    }

    public void ExitToMenu() => UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    public void Pause()
    {
        GameIsPaused = true;
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }
    
    public void Resume()
    {
        GameIsPaused = false;
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void PauseResumeAction(InputAction.CallbackContext Context)
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}

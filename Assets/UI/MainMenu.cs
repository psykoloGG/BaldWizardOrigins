using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button DeleteSaveButton;
    public Button Level2Button;
    public Button Level3Button;
    
    private void Awake()
    {
        if (SaveSystem.DataExists())
        {
            DeleteSaveButton.interactable = true;
            if (SaveSystem.LoadData().Level >= 2)
            {
                Level2Button.interactable = true;
            }
            if (SaveSystem.LoadData().Level >= 3)
            {
                Level3Button.interactable = true;
            }
        }
        else
        {
            DeleteSaveButton.interactable = false;
        }
    }

    public void StartGame(int Index)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(Index);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void DeleteSave()
    {
        SaveSystem.DeleteData();
        DeleteSaveButton.interactable = false;
        
        if (Level2Button != null)
        {
            Level2Button.interactable = false;
        }
        
        if (Level3Button != null)
        {
            Level3Button.interactable = false;
        }
    }
}

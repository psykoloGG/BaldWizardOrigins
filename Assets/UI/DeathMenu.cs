using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public GameObject DeathMenuPanel;
    
    public void ShowDeathMenu()
    {
        DeathMenuPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }
    
    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}

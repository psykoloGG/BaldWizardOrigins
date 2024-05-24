using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicGate : MonoBehaviour
{
    public BoxCollider2D GateCollider;
    public GameObject WinMenu;
    
    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            Win();
        }
    }

    private void Win()
    {
        SaveSystem.SaveData(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1, 0, "None");
        WinMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVolumeBehavior : MonoBehaviour
{
    public GameObject DeathMenu;
    
    private void OnTriggerEnter2D(Collider2D Other)
    {
        Debug.Log("Death Volume Triggered");
        if (Other.CompareTag("Player"))
        {
            DeathMenu.SetActive(true);
            Destroy(Other.gameObject);
            Time.timeScale = 0.0f;
        }
    }
}

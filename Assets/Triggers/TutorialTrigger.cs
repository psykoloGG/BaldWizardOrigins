using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject TutorialText;
    public bool ShouldEnable = false;
    
    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            if (ShouldEnable)
            {
                TutorialText.SetActive(true);
            }
            else
            {
                TutorialText.SetActive(false);
            }
        }
    }
}

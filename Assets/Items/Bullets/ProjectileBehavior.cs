using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class will mostly be container for info about the bullet at it's creation (aka carying the gun info)
public class ProjectileBehavior : MonoBehaviour
{
    // Bullet definition (stats)
    [HideInInspector] public Gun Gun;

    public Gun GetGun()
    {
        if (Gun)
        {
            return Gun;
        }

        return null;
    }
}
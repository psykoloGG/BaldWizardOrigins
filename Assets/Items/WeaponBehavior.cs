using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    // Weapon definition (stats)
    [SerializeField] private Weapon Weapon;
    [SerializeField] private SpriteRenderer WeaponSpiritRenderer;
    [SerializeField] private Collider2D PickupZone;

    public void Awake()
    {
        WeaponSpiritRenderer.sprite = Weapon.Icon;
    }

    public Weapon GetWeapon()
    {
        if (Weapon)
        {
            return Weapon;
        }
        return null;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeaponSlot : MonoBehaviour
{
    private SpriteRenderer HandSpriteRenderer;
    private Weapon CurrentWeapon;

    private void Awake()
    {
        HandSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Fire()
    {
        if (!CurrentWeapon)
        {
            return;
        }

        if (CurrentWeapon.TimeBetweenAttacks >= 1 / CurrentWeapon.AttackRate)
        {
            CurrentWeapon.Activate(transform);
            CurrentWeapon.TimeBetweenAttacks = 0;
        }
    }

    public void Equip(Weapon Weapon)
    {
        CurrentWeapon = Weapon;
        HandSpriteRenderer.sprite = CurrentWeapon.Icon;
    }

    private void FixedUpdate()
    {
        if (CurrentWeapon)
        {
            CurrentWeapon.TimeBetweenAttacks += Time.deltaTime;
        }
    }
}
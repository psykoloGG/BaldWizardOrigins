
using UnityEngine;

public abstract class RangedWeapon : Weapon
{
    [Header("Ranged Weapon Stats")]
    public GameObject ProjectilePrefab;
    public float ProjectileSpeed;
    public float ProjectileLifetime;
}
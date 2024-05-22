using UnityEngine;

public abstract class Weapon : Item
{
    [Header("Weapon Stats")]
    public int Damage;
    public float AttackRate;
    
    [HideInInspector]
    public float TimeBetweenAttacks;
    
    public abstract void Activate(Transform FirePoint);
}
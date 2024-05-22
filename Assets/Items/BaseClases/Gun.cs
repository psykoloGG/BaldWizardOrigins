using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGun", menuName = "Weapons/Gun")]
public class Gun : RangedWeapon
{
    public override void Activate(Transform FirePoint)
    {
        if (TimeBetweenAttacks >= 1 / AttackRate)
        {
            GameObject Bullet = Instantiate(ProjectilePrefab, FirePoint.position, FirePoint.rotation);
            Bullet.GetComponent<ProjectileBehavior>().Gun = this;
            Bullet.GetComponent<Rigidbody2D>().velocity = FirePoint.right * ProjectileSpeed;
            Destroy(Bullet, ProjectileLifetime);

            TimeBetweenAttacks = 0;
        }
    }
}
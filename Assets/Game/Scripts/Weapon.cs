using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    #region fields
    private float damage;
    private float attackCooldown;
    private bool isMelee;
    private Collider hitbox;
    private Projectile projectile;
    private float projectileSpeed;
    #endregion

    #region methods
    public abstract void Attack(); //TODO: make this virtual, to handle projectile firing
    //i.e if this Weapon has a projectile, do (that projectile).spawn in attack()
    
    #endregion
}

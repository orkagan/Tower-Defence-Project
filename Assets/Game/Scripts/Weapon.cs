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
    public virtual void Attack()
    { 
    
    }
    //TODO: if this Weapon has a projectile, do (that projectile).spawn in attack()
    
    #endregion
}

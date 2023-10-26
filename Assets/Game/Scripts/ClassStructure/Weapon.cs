using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    #region fields
    public float damage;
    public float attackCooldown;
    public bool isMelee;
    public Collider hitbox;
    public Projectile projectile;
    public float projectileSpeed;
    #endregion

    #region methods
    public virtual void Attack(Vector3 direction, Vector3 position)
    {
        Debug.Log("Attacked");

        if (projectile != null)
        {
            projectile.Spawn(direction, position);
        }
    }
    //TODO: if this Weapon has a projectile, do (that projectile).spawn in attack()
    
    #endregion
}

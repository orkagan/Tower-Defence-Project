using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : AggressiveEntity
{
    #region Fields
    public string entityName;
    public float damage;
    public int cost;
    public float attackCooldown;
    public Projectile projectile;
    public float projectileSpeed;
    public float range;
    public int upgrades;
    #endregion
}

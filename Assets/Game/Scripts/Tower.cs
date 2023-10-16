using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : AggresiveEntity
{
    #region Fields
    private float damage;
    private int cost;
    private float attackCooldown;
    //private Projectile projectile;
    private float projectileSpeed;
    private float range;
    private int upgrades;
    #endregion
}

using System;
using UnityEngine;

public class Tower : AggressiveEntity
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

    public override void Attack()
    {
        Transform target = FindTarget().transform;
    }

    public override Collider FindTarget()
    {
        throw new NotImplementedException();
    }

    private void OnValidate()
    {
        gameObject.name = entityName;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Entities/Aggressive Entities/Tower")]
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
        throw new System.NotImplementedException();
    }

    public override Collider FindTarget()
    {
        throw new System.NotImplementedException();
    }
}

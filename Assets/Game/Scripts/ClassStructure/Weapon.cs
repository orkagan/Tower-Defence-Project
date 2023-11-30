using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    #region Fields    
    public float damage;
    public float attackCooldown;
    public bool isMelee;
    public Collider hitbox;
    public Projectile projectile;
    public float projectileSpeed;
    #endregion

    #region Methods
    public virtual void Attack(Vector3 direction, Vector3 position)
    {
        Debug.Log("Attacked");

        if (projectile != null)
        {
            Projectile spawnedProjectile;
            //projectile.Spawn(direction, position);
            Vector3 aimDirection = direction;
            Vector3 spawnPosition = position + aimDirection.normalized;
            Debug.DrawRay(spawnPosition, aimDirection, Color.black, 3f);
            spawnedProjectile = Instantiate(projectile, spawnPosition, Quaternion.identity);
            spawnedProjectile.direction = aimDirection;
        }
    }
    //TODO: if this Weapon has a projectile, do (that projectile).spawn in attack()
    
    #endregion
}

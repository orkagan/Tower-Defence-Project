using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Fields    
    public float damage;
    public float attackCooldown;
    public bool isMelee;
    public Projectile projectile;
    public float projectileSpeed;
    
    Collider hitbox;
    #endregion

    #region Methods
    public void Attack(Vector3 aimDirection, Vector3 position)
    {
        Debug.Log("Attacked");

        if (projectile != null)
        {
            projectile.Spawn(aimDirection, position);

            Vector3 spawnPosition = position + aimDirection.normalized;

            Debug.DrawRay(spawnPosition, aimDirection, Color.black, 3f);

            projectile = Instantiate(projectile, spawnPosition, Quaternion.identity);
            projectile.direction = aimDirection;
        }
    }
    //TODO: if this Weapon has a projectile, do (that projectile).spawn in attack()

    private void OnValidate()
    {
        projectile.damage = damage;
    }

    #endregion
}

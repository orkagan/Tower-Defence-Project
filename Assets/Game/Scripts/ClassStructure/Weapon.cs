using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    #region Fields    
    public float damage;
    public int attackCooldown;
    public bool isMelee;
    public Collider hitbox;
    public Projectile projectile;
    public float projectileSpeed;
    #endregion

    #region Methods

    /*handling attack delay
    basically there are 2 delays
    how often you can click, which then plays the animation, until you can click again
    and how often "attack", which will almost always be the same number
    but if you wanted to make something that "attacked" three times in one click, you'd make attack = click/3, so the game runs attack for as long as clicked is true


    */


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
            
            spawnedProjectile.damage += damage; // i wonder if this will cause the first frame of the thing to not have added wep damage. Oh well!
            spawnedProjectile.initialSpeed += projectileSpeed; //this will be a good test for that
            spawnedProjectile.direction = aimDirection;
        }
    }
    //TODO: if this Weapon has a projectile, do (that projectile).spawn in attack()

    #endregion
}

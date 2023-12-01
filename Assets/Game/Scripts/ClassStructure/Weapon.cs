using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Fields    
    public float damage;
    public int attackCooldown;
    //public bool isMelee;
    public Collider hitbox;
    public Projectile projectile;
    public float projectileSpeed;
    public float fireRate;
    private float _fireRate;
    #endregion

    #region Methods  
    private void Awake()
    {
        _fireRate = fireRate;
    }

    public void Attack(Vector3 aimDirection, Vector3 position)
    {
        if (GameStateHandler.Instance.GetCurrentState != GameState.AttackPhase)
            return;

        _fireRate--;

        Debug.Log("Attacked");

        if (projectile != null && _fireRate <= 0)
        {
            //projectile.Spawn(direction, position);
            Vector3 spawnPosition = position + aimDirection.normalized;
            Debug.DrawRay(spawnPosition, aimDirection, Color.black, 3f);

            Projectile spawnedProjectile;
            spawnedProjectile = Instantiate(projectile, spawnPosition, Quaternion.identity);

            spawnedProjectile.damage += damage; // i wonder if this will cause the first frame of the thing to not have added wep damage. Oh well!
            spawnedProjectile.initialSpeed += projectileSpeed; //this will be a good test for that
            spawnedProjectile.direction = aimDirection;

            _fireRate = fireRate;
        }
    }
    #endregion
}

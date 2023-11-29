using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region Fields
    public float speed;
    public float maxSpeed;
    public bool hasGravity;
    public float gravityScale;
    public float lifespan;
    public int maxHits;
    public int hits;
    public float initialSpeed;
    public Rigidbody rb;
    public Collider hitbox;
    public float damage;
    public Vector3 direction;
    #endregion

    #region Methods


    public virtual void Hit(Enemy enemy) 
    {
        
        enemy.DecreaseHealth((int)damage); //int conversion is stupid
        hits++;
        if (hits >= maxHits)
        {
            Die();
        }
    }

    
    public virtual void Spawn() //this could be tonnes of things
    {
        
        
        rb.AddForce(direction * initialSpeed, ForceMode.VelocityChange);// i think this is applying to the prefab and not the instance somehow
       
        
       
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
    #endregion

    #region Unity Methods

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("DEBUG: projectile entered trigger");
        Enemy enemyHit = other.transform.root.GetComponent<Enemy>();

        Hit(enemyHit);
    }

    public virtual void Awake()
    {
        Debug.Log("Projectile Spawned");
        

    }

    public virtual void Start()
    {
        Spawn();
    }
    #endregion 
}

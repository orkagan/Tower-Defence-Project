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
    #endregion

    #region Methods
    public virtual void Hit() 
    {
    
    }
    public virtual void Spawn(Vector3 direction, Vector3 position) //this could be tonnes of things
    {
        
        Debug.Log("Projectile Spawn");
        Vector3 aimDirection = direction;
        Vector3 spawnPosition = position + aimDirection.normalized;
        Debug.DrawRay(spawnPosition, aimDirection, Color.black, 3f);
        Instantiate(this, spawnPosition, Quaternion.identity);
        rb.AddForce(aimDirection * 50f, ForceMode.VelocityChange);// i think this is applying to the prefab and not the instance somehow
       
        
       
    }
    public virtual void Die()
    { 
    
    }
    #endregion

    #region Unity Methods

    public virtual void Awake()
    { 
    
    }
    #endregion 
}

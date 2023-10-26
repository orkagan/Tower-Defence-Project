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
    public virtual void Hit() 
    {
    
    }

    
    public virtual void Spawn() //this could be tonnes of things
    {
        
        
        rb.AddForce(direction * 50f, ForceMode.VelocityChange);// i think this is applying to the prefab and not the instance somehow
       
        
       
    }
    public virtual void Die()
    { 
    
    }
    #endregion

    #region Unity Methods

    public virtual void Awake()
    {
        Debug.Log("bruh");
        

    }

    public virtual void Start()
    {
        Spawn();
    }
    #endregion 
}

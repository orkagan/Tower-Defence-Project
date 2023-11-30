using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    #region Fields

    #region Not Used
    //public float speed;
    //public float maxSpeed; 
    //public bool hasGravity;
    //public float gravityScale;
    #endregion

    private float lifespan;
    public float maxLifespan;

    public int maxHits;
    public int hits;
    public int hitRate; //SET HIT RATE TO ZERO FOR WEAPONS WITH ONLY ONE HIT OR FOR INSTANT???
    private int hitRateTimer;

    public float initialSpeed;

    public float damage;

    public Rigidbody rb;
    public Collider hitbox;

    public Vector3 direction;
    #endregion

    #region Methods

    public virtual void Hit(Enemy enemy) 
    {        
        enemy?.DecreaseHealth((int)damage); //int conversion is stupid
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

        if (!other.CompareTag("Enemy"))
            return;

        Enemy enemyHit = other.GetComponent<Enemy>();

        if (hitRateTimer == 0)
        {
            Hit(enemyHit);
            hitRateTimer = hitRate;
        }        
    }

    public virtual void Awake()
    {
        Debug.Log("Projectile Spawned");       
    }
    public virtual void FixedUpdate()
    {
        if (lifespan >= maxLifespan) //TODO: fix weapon inventory so bullshit like this doesnt delete orig
        {
            Die();
        }

        else
        {
            lifespan++;
        }

        if (hitRateTimer <= 0 == false)
        {
            hitRateTimer--;
        }
    }
    public virtual void Start()
    {
        Spawn();
    }
    #endregion 
}

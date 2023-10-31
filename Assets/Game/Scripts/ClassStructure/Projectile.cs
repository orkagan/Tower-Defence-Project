using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region Fields
    public float speed;
    public float maxSpeed;
    public bool hasGravity;
    public float gravityScale;
    public float maxDuration;
    public float duration;
    public int maxHits;
    public int hits;
    public float initialSpeed;
    public Rigidbody rb;
    public Collider hitbox;
    public float damage;
    public Vector3 direction;
    #endregion

    #region Methods

    public virtual void SpeedControl()
    {

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //horizontal velocity
        if (flatVel.magnitude > maxSpeed)
        {
            Debug.Log("Speedcheck: limited velocity");
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        if (flatVel.magnitude < 0.01f)
        {
            rb.velocity = Vector3.zero;
        }

    }

    public virtual void DurationControl()
    {
        if (duration >= maxDuration)
        {
            Die();
        }

        else
        {
            duration += 1;
        }
    }
    public virtual void Hit()
    {

    }


    public virtual void Spawn() //this could be tonnes of things
    {


        rb.AddForce(direction * initialSpeed, ForceMode.VelocityChange);
        
    }
    public virtual void Die()
    {
        GameObject inGameInstance = gameObject;
        Destroy(inGameInstance);
      
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

    public virtual void FixedUpdate()
    {
        SpeedControl();
        DurationControl();
    }
    #endregion 
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region Fields
    public float speed;//currently not being used
    public float maxSpeed; //being used in speedControl
    public float initialSpeed;

    public bool hasGravity;//not implemented, might not ever because iso?
    public float gravityScale;//not implemented, might not even because iso?
    
    public float maxDuration;//being used in durationControl
    public float duration;//being used in durationcontrol
    
    public int maxHits;//need to implement enemies
    public int hits;//need to implement enemies
    
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
        //What happens when a projectile hits a wall? what happens when it hits an enemy?
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region Fields
    private float speed;
    private float maxSpeed;
    private bool hasGravity;
    private float gravityScale;
    private float lifespan;
    private int maxHits;
    private int hits;
    private float initialSpeed;
    private Rigidbody rigidBody;
    private Collider hitbox;
    private float damage;
    #endregion

    #region Methods
    public abstract void Hit(); //we might want these to be virtual, not abstract
    public abstract void Spawn();
    public abstract void Die();
    #endregion
}

using System;
using UnityEngine;

public class Enemy : AggressiveEntity
{
    #region Fields
    private Transform enemyTransform;
    private Rigidbody enemyRB;
    private ParticleSystem enemyPS;
    private float moveSpeed;
    private int resourceDrop; //we might want to dynamically calculate this during death
    private EnemyState enemyState;
    #endregion
    
    #region Properties

    public float GetHealth
    { 
        get => health;
        set => health = value;
    }
    #endregion

    #region Methods

    public void DecreaseHealth(int decrement) => GetHealth -= decrement;
    
    public static void CalculateState() //i've decided i'll make these static so i don't have to redo them
    { 
    
    }
    #endregion
    enum EnemyState
    {
        Attacking,
        Rushing
    }

    public override void Attack()
    {
        throw new NotImplementedException();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : AggresiveEntity
{
    #region Fields
    private Transform enemyTransform;
    private Rigidbody enemyRB;
    private ParticleSystem enemyPS;
    private float moveSpeed;
    private int resourceDrop; //we might want to dynamically calculate this during death
    private EnemyState enemyState;
    #endregion

    #region Methods
    public static void CalculateState() //i've decided i'll make these static so i don't have to redo them
    { 
    
    }
    #endregion
    enum EnemyState
    {
        Attacking,
        Rushing
    }
}

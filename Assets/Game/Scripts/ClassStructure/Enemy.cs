using System;
using UnityEngine;

public class Enemy : AggressiveEntity
{
    #region Fields
    //protected Transform _enemyTransform;
    //protected Rigidbody _enemyRB;
    //protected ParticleSystem _enemyPS;
    //[SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected int _resourceDrop; //we might want to dynamically calculate this during death
    [SerializeField] protected EnemyState _enemyState;
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

    public enum EnemyState
    {
        Attacking,
        Rushing
    }

    public override void Attack()
    {
        throw new NotImplementedException();
    }
}

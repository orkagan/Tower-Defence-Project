using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyState
{
    Attacking,
    Rushing
}

public class Enemy : AggressiveEntity
{
    #region Fields
    [Header("Enemy Fields")]
    //protected Transform _enemyTransform;
    //protected Rigidbody _enemyRB;
    //protected ParticleSystem _enemyPS;
    //[SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected int _resourceDrop;
    [SerializeField] protected EnemyState _enemyState;

    [SerializeField] private Text _healthText;
    #endregion

    #region Properties

    SphereCollider _attackSphere => GetComponent<SphereCollider>();
    private HUDManager activeHUD
    {
        get
        {
            HUDManager activeHUD = FindObjectOfType<HUDManager>();
            return activeHUD;
        }
    }
    /// <summary>
    /// This enemy's maximum health.
    /// </summary>
    public float GetMaxHealth => _maximumHealth;

    /// <summary>
    /// This enemy's attack range.
    /// </summary>
    public float GetRange => _attackRange;

    private string HealthText
    {
        set => _healthText.text = value;
    }

    #endregion

    #region Methods
    #region Unity Methods
    private void Start()
    {
        GetHealth = GetMaxHealth;

        onDeath.AddListener(() => activeHUD?.SetResourceCount(_resourceDrop, false));
    }

    private void Update()
    {
        UpdateHealth();
    }

    private void OnValidate()
    {
        _attackSphere.radius = _attackRange;
    }
    #endregion

    private void UpdateHealth()
    {
        string newHealth = GetHealth.ToString();
        HealthText = $"{newHealth}/{GetMaxHealth}";

        if (GetHealth <= 0)
        {
            StartCoroutine(nameof(Die));
        }
    }

    public override IEnumerator Die()
    {
        onDeath.Invoke();
        
        DestroyImmediate(gameObject);
        
        return base.Die();
    }

    public override void Attack()
    {
        throw new NotImplementedException();
    }

    public void DecreaseHealth(int decrement) => GetHealth -= decrement;
    #endregion
}
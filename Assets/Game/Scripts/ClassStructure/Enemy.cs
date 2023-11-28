using System;
using System.Collections;
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
    [SerializeField] private CapsuleCollider _attackCollider;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected int _resourceDrop;
    [SerializeField] protected EnemyState _enemyState;

    [SerializeField] private Text _healthText;
    #endregion

    #region Properties

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

    private void Start()
    {
        GetHealth = GetMaxHealth;

        onDeath.AddListener(() => activeHUD.SetResourceCount(_resourceDrop));
    }

    private void Update()
    {
        UpdateHealth();
    }

    private void OnValidate()
    {
        _attackCollider.radius = GetRange;
    }

    private void UpdateHealth()
    {
        string newHealth = GetHealth.ToString();
        HealthText = $"{newHealth}/{GetMaxHealth}";

        if (GetHealth <= 0)
        {
            StartCoroutine(nameof(Die));
        }
    }

    public void DecreaseHealth(int decrement) => GetHealth -= decrement;

    public override IEnumerator Die()
    {
        onDeath.Invoke();
        
        DestroyImmediate(gameObject);
        
        return base.Die();
    }

    public override void Attack()
    {
        
    }

    //public static void CalculateState() //i've decided i'll make these static so i don't have to redo them
    //{
    //    
    //}

    #endregion
}
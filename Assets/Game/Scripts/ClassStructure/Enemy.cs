using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public enum EnemyState
{
    Attacking,
    Rushing
}

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

    [SerializeField] private Text _healthText;

    private HUDManager activeHUD
    {
        get
        {
            HUDManager activeHUD = GameObject.FindObjectOfType<HUDManager>();
            return activeHUD;
        }
    }
    #endregion

    #region Properties
    public float GetHealth
    {
        get => health;
        set => health = value;
    }

    public float GetMaxHealth => _maximumHealth;

    private string HealthText
    {
        get => _healthText.text;
        set
        {
            HealthText = value;
            _healthText.text = HealthText;
        }
    }
    #endregion

    #region Methods
    private void Start()
    {
        GetHealth = GetMaxHealth;

        onDeath.AddListener(() => activeHUD.GetResourceCount = 1);
    }

    private void Update()
    {
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        string newHealth = GetHealth.ToString();
        HealthText = $"{newHealth}/{GetMaxHealth}";
    }

    public void DecreaseHealth(int decrement) => GetHealth -= decrement;

    public static void CalculateState() //i've decided i'll make these static so i don't have to redo them
    {

    }

    public override IEnumerator Die()
    {
        return base.Die();
    }

    public override void Attack()
    {
        throw new NotImplementedException();
    }
    #endregion
}

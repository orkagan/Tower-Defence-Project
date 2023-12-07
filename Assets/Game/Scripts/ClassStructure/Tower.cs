using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Tower : AggressiveEntity
{
    #region Fields

    [SerializeField] private GameObject _towerDisplayName;
    [SerializeField] private string _tagName;
    [SerializeField] private string _entityName;

    [SerializeField, Space(10)] private Weapon _weapon;
    [SerializeField] private Transform _shootPoint;

    [SerializeField, Space(10)] private float _range;
    [SerializeField] private float _attackCooldown;
    float cooldown;
    [SerializeField] private float _damage;

    [SerializeField, Space(10)] private int _cost;

    [SerializeField] private List<Collider> _enemiesInRange = new List<Collider>();
    #endregion

    #region Properties
    /// <summary>
    /// The name of the Tower.
    /// </summary>
    public string GetName => _entityName;

    /// <summary>
    /// The number of resources(int) required to place/upgrade a tower.
    /// </summary>
    public int GetCost => _cost;

    /// <summary>
    /// The damage the tower deals towards enemies.
    /// </summary>
    public float GetDamage
    {
        get => _damage;
        private set => _damage = value;
    }

    /// <summary>
    /// The number of seconds(float) between each tower's attack.
    /// </summary>
    public float GetAttackCooldown
    {
        get => _attackCooldown;
        private set
        {
            _attackCooldown = value;
            _attackCooldown = Mathf.Clamp(_attackCooldown, 0.1f, int.MaxValue);
        }
    }

    /// <summary>
    /// The distance around the tower - how far around the tower it searches for enemies.
    /// </summary>
    public float GetRange
    {
        get => _range;
        private set => _range = value;
    }

    private BoxCollider Box => GetComponent<BoxCollider>();
    private HUDManager hud => FindAnyObjectByType<HUDManager>();
    #endregion   

    #region Methods 
    #region Unity Methods
    private void Start()
    {
        cooldown = GetAttackCooldown;
    }

    private void Update()
    {
        if (GameStateHandler.Instance.GetCurrentState == GameState.AttackPhase)
        {
            cooldown -= Time.deltaTime;

            if (_enemiesInRange.Count > 0 && cooldown <= 0)
            {
                Attack();

                cooldown = GetAttackCooldown;
            }
        }
    }

    private void OnValidate()
    {
        name = GetName;
        _towerDisplayName.GetComponent<Text>().text = GetName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(_tagName)) return;

        _enemiesInRange.Add(other);

        Enemy e = other.GetComponent<Enemy>();
        e.onDeath.AddListener(() => _enemiesInRange.Remove(other));
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag(_tagName)) return;

        _enemiesInRange.Remove(other);
    }
    #endregion
    #region Upgrades
    public void IncreaseRange(float increase)
    {
        if (hud.GetResourceCount > 0)
        {
            GetRange += increase;

            Vector3 bs = Box.size;
            bs.z = bs.x = GetRange;
            Box.size = bs;
        }
    }

    public void IncreaseDamage(float increase)
    {
        if (hud.GetResourceCount > 0)
            GetDamage += increase;
    }

    public void DecreaseAttackCooldown(float decrease)
    {
        if (hud.GetResourceCount > 0)
            GetAttackCooldown -= decrease;
    }
    #endregion

    public override void Attack()
    {
        StartCoroutine(nameof(Shoot));
    }

    private IEnumerator Shoot()
    {
        foreach (Collider enemy in _enemiesInRange)
        {
            Vector3 direction = enemy.transform.position - _shootPoint.position;
            _weapon.Attack(direction, _shootPoint.position);
        }

        yield return new WaitForSeconds(GetAttackCooldown);
    }
    #endregion
}
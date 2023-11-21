using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : AggressiveEntity
{
    #region Fields

    [SerializeField] private GameObject _towerDisplayName;
    [SerializeField] private string _tagName;
    [SerializeField] private string _entityName;

    [SerializeField] private float _damage;
    [SerializeField] private int _cost;

    [SerializeField] private float _attackCooldown;

    //[SerializeField] private Projectile _projectile;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _range;
    [SerializeField] private Transform _shootFrom;

    private List<Collider> _enemiesInRange;

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
        set => _damage = value;
    }

    /// <summary>
    /// The number of seconds(float) between each tower's attack.
    /// </summary>
    public float GetAttackCooldown
    {
        get => _attackCooldown;
        set => _attackCooldown = value;
    }

    /// <summary>
    /// The distance around the tower - where the tower searches for enemies.
    /// </summary>
    public float GetRange
    {
        get => _range;
        private set => _range = value;
    }

    #endregion

    #region Unity Methods

    private void Update()
    {
        Attack();
    }

    private void OnValidate()
    {
        name = GetName;
        _towerDisplayName.GetComponent<Text>().text = GetName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(_tagName)) return;

        
        //_enemiesInRange.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag(_tagName)) return;

        //_enemiesInRange.Remove(other);
    }

    #endregion

    #region Methods

    #region Upgrades

    public void SetRange(int increase)
    {
        GetRange += increase;
    }

    public void SetDamage(int increase)
    {
        GetDamage += increase;
    }

    public void SetAttackCooldown(int decrease)
    {
        GetAttackCooldown -= decrease;
    }

    #endregion

    public override void Attack()
    {
        StartCoroutine(nameof(Shoot));
    }

    private IEnumerator Shoot()
    {
        if (_enemiesInRange.Count > 0)
        {
            foreach (Collider enemy in _enemiesInRange)
            {
                //shoot a projectile from top of tower and onto target
                Vector3 shootPoint = _shootFrom.position;
                GameObject newBullet = Instantiate(_projectile, shootPoint, Quaternion.identity, transform);
                Vector3 direction = shootPoint - enemy.transform.position;

                newBullet.transform.position += direction * (Time.deltaTime * _projectileSpeed);
            }
        }

        yield return new WaitForSeconds(GetAttackCooldown);
    }

    #endregion
}
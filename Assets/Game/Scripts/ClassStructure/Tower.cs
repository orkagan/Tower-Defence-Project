using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(CheckForNearbyObjects))]
public class Tower : AggressiveEntity, IPointerEnterHandler, IPointerExitHandler
{
    #region Fields

    [Header("Polish")]
    [SerializeField] private GameObject _towerDisplayName;
    
    [Header("Main Variables")]
    [SerializeField] private string _entityName;
    [SerializeField] private float _damage;
    [SerializeField] private int _cost;
    [SerializeField] private float _attackCooldown;
    //[SerializeField] private Projectile _projectile;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _range;
    [SerializeField] private Transform _shootFrom;
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
    /// <summary>
    /// Connected component which finds nearby enemies within range.
    /// </summary>
    private CheckForNearbyObjects EnemyFinder => GetComponent<CheckForNearbyObjects>();
    #endregion

    #region Unity Methods
    private void Start()
    {
        _towerDisplayName.SetActive(false);
    }

    private void Update()
    {
        Attack();
    }

    private void OnValidate()
    {
        name = GetName;
        _towerDisplayName.GetComponent<Text>().text = GetName;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _towerDisplayName.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _towerDisplayName.SetActive(false);
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
        Transform target = FindTarget().transform;
        
        //shoot a projectile from top of tower and onto target
        Vector3 shootPoint = _shootFrom.position;
        GameObject newBullet = Instantiate(_projectile, shootPoint, Quaternion.identity, transform);
        Vector3 direction = shootPoint - target.position;
        
        newBullet.transform.position += direction * (Time.deltaTime * _projectileSpeed);
    }

    //refer to script which finds any enemies that are in range
    public override Collider FindTarget()
    {
        Collider enemy = GetComponent<CheckForNearbyObjects>().enemyCollider;
        return enemy;
    }
    #endregion
}

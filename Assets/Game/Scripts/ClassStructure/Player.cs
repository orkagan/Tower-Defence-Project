using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    //hardcoded pc inputs on line 39
    
    #region Fields

    [Header("Player Fields")]
    [SerializeField] private string _tagName = "Enemy";
    [SerializeField] private CapsuleCollider _attackCollider;
    [SerializeField] private float _attackRange = 3f;
    public int currency;
    //[SerializeField] private Tower[] towers;
    //[SerializeField] private Weapon[] weapons;
    [SerializeField] private bool readyToBeginWave;

    private List<Collider> _enemiesInRange = new List<Collider>();
    public Vector3 orientation;
    #endregion

    #region Methods

    #region Unity Methods
    private void Start()
    {
        _attackCollider.radius = _attackRange;
        
        //when player dies, sends message on chat log of such
        onDeath.AddListener(() =>
            ChatHandler.Instance.CreateNewLine("Player has died."));
    }

    public void Update()
    {
        Debug.DrawRay(this.transform.position, orientation);

        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }
    
    //when an enemy enters attack range
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(_tagName)) return;

        _enemiesInRange.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag(_tagName)) return;

        _enemiesInRange.Remove(other);
    }
    #endregion
    
    public void Attack()
    {
        foreach (Collider enemy in _enemiesInRange)
        {
            Enemy e = enemy.GetComponent<Enemy>();
            e.DecreaseHealth(5);

            if (e.GetHealth <= 4) _enemiesInRange.Remove(enemy);
        }
    }

    //public void ReadyUp()
    //{ 
    //
    //}

    //public void UpgradeWeapon(Weapon weapon)
    //{ 
    //
    //}

    public override IEnumerator Die()
    {
        onDeath.Invoke();
        
        DestroyImmediate(gameObject);
        
        return base.Die();
    }

    //public void UpgradeTower(Tower tower)
    //{ 

    //}
    #endregion
}

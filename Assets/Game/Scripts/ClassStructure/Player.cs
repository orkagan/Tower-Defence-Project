using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public struct WeaponStash
{
    public Weapon weapon;
    public Projectile projectile;
    [Tooltip("Rate of fire per second")] public float fireRate;
}

public class Player : Entity
{
    #region Fields
    [Header("Player Fields")]
    [SerializeField] private InputMaster _controls;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int _currency = 10;
    //[SerializeField] private Tower[] towers;
    [SerializeField] private WeaponStash[] weapons;
    [SerializeField] private bool readyToBeginWave;

    public int attackDelay; //this is handled by the player because reasons

    public Vector3 orientation;
    #endregion

    #region Properties
    public int Currency
    {
        get => _currency;
        set
        {
            _currency = value;
            _currency = Mathf.Clamp(Currency, 0, int.MaxValue);
        }
    }
    #endregion

    #region Methods
    #region Unity Methods
    private void Awake()
    {
        _controls = new InputMaster();
    }
    private void OnEnable()
    {
        _controls.Enable();
            _controls.Player.Attack.performed += Attack;
    }

    private void OnDisable()
    {
        _controls.Disable();
        _controls.Player.Attack.performed -= Attack;
    }

    private void Start()
    {
        onDeath.AddListener(() =>
            Debug.Log("Player has died."));
    }

    public void Update()
    {
        float bungle = _controls.Player.Attack.ReadValue<float>();

        if (Mathf.Abs(bungle) >= 0.1f)
        {
            Attack();
        }
        //Debug.DrawRay(this.transform.position, orientation);


        //if (controls.Player.Attack.performed)
        //{
        //    Attack();
        //}
    }

    private void FixedUpdate()
    {
        if (attackDelay <= 0 == false)
        {
            attackDelay--;
        }
    }

    private void OnValidate()
    {
        foreach (WeaponStash ws in weapons)
        {
            Weapon w = ws.weapon;

            w.fireRate = ws.fireRate;
            w.projectile = ws.projectile;
        }
    }
    #endregion

    public void Attack(InputAction.CallbackContext value)
    {
        if (attackDelay == 0)
        {
            Weapon w = weapons[0].weapon;

            w.Attack(orientation, transform.position);
            attackDelay = w.attackCooldown;
        }
    }

    public void Attack()
    {
        if (attackDelay == 0)
        {
            Weapon w = weapons[0].weapon;

            w.Attack(orientation, transform.position);
            attackDelay = w.attackCooldown;
        }
    }

    public void ReadyUp()
    {
        readyToBeginWave = true;
    }

    public void UpgradeWeapon(Weapon weapon)
    {

    }

    public override IEnumerator Die()
    {
        onDeath.Invoke();

        DestroyImmediate(gameObject);

        return base.Die();
    }

    public void LoseHealth(int decrement)
    {
        GetHealth -= decrement;
    }

    //public void UpgradeTower(Tower tower)
    //{ 

    //}
    #endregion
}

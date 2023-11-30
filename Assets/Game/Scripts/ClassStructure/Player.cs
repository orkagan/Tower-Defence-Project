using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    #region Fields
    [Header("Player Fields")]
    [SerializeField] private InputMaster _controls;
    [SerializeField] private float moveSpeed;
    public int currency;
    //[SerializeField] private Tower[] towers;
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private bool readyToBeginWave;

    public int attackDelay; //this is handled by the player because reasons



    public Vector3 orientation;
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
    #endregion

    public void Attack(InputAction.CallbackContext value)
    {
        if (attackDelay == 0)
        {
            weapons[0].Attack(orientation, transform.position);
            attackDelay = weapons[0].attackCooldown;
        }

    }

    public void Attack()
    {
        if (attackDelay == 0)
        {
            weapons[0].Attack(orientation, transform.position);
            attackDelay = weapons[0].attackCooldown;
        }
    }

    public void ReadyUp()
    {

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

    //public void UpgradeTower(Tower tower)
    //{ 

    //}
    #endregion
}

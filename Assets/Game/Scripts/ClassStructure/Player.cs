using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    #region Fields
    [Header("Player Fields")]
    public InputMaster controls;
    [SerializeField] private float moveSpeed;
    public int currency;
    //[SerializeField] private Tower[] towers;
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private bool readyToBeginWave;

    public Vector3 orientation;
    #endregion

    #region Methods
    #region Unity Methods
    private void Awake()
    {
        controls = new InputMaster();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Attack.performed += Attack;
    }

    private void OnDisable()
    {
        controls.Player.Attack.performed -= Attack;
        controls.Disable();
    }

    private void Start()
    {
        onDeath.AddListener(() =>
            ChatHandler.Instance.CreateNewLine("Player has died."));
    }

    public void Update()
    {
        float bungle = controls.Player.Attack.ReadValue<float>();

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
    #endregion

    public void Attack(InputAction.CallbackContext value)
    {
        Debug.Log("Pew");

        if (GameStateHandler.Instance.GetCurrentState == GameState.AttackPhase)
        {
            weapons[0].Attack(orientation, transform.position);
        }
    }

    public void Attack()
    {
        weapons[0].Attack(orientation, transform.position);
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

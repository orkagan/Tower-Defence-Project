using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    #region Fields
    public InputMaster controls;
    public float moveSpeed;
    public int currency;
    public Tower[] towers;
    public Weapon[] weapons;
    public bool readyToBeginWave;
    public Vector3 orientation; //this comes from PlayerMovement
    #endregion

    #region Methods
    public void Attack(InputAction.CallbackContext value)
    {
        weapons[0].Attack(orientation, this.transform.position);
    }
    public void Attack()
    {
        weapons[0].Attack(orientation, this.transform.position);
    }


    public void ReadyUp()
    {

    }

    public void UpgradeWeapon(Weapon weapon)
    {

    }

    public void UpgradeTower(Tower tower)
    {

    }


    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Attack.performed += Attack;
      
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.Player.Attack.performed -= Attack;
       
        
    }

    private void Awake()
    {
        controls = new InputMaster();
    }

    public void Update()
    {
        float bungle = controls.Player.Attack.ReadValue<float>();
        if (Mathf.Abs(bungle) >= 0.1)
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
}

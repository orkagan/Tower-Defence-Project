using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entities/Player", fileName = "New Player")]
public class Player : Entity
{
    #region Fields
    public float moveSpeed;
    public int currency;
    public Tower[] towers;
    public Weapon[] weapons;
    public bool readyToBeginWave;
    public Vector3 orientation; //this comes from PlayerMovement
    #endregion

    #region Methods
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

    public void Update()
    {
        Debug.DrawRay(this.transform.position, orientation);

        if (Input.GetKeyDown(KeyCode.T))
        {
            Attack();
        }
    }
    #endregion
}

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
    #endregion

    #region Methods
    public void Attack()
    { 
    
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
}

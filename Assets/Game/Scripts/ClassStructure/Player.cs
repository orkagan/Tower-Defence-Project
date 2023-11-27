using System.Collections;
using UnityEngine;

public class Player : Entity
{
    #region Fields
    [SerializeField] private float moveSpeed;
    public int currency;
    //[SerializeField] private Tower[] towers;
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private bool readyToBeginWave;
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

    public override IEnumerator Die()
    {
        return base.Die();
    }

    //public void UpgradeTower(Tower tower)
    //{ 

    //}
    #endregion
}

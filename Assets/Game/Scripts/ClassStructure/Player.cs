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

    public Vector3 orientation;
    #endregion

    #region Methods
    public void Update()
    {
        Debug.DrawRay(this.transform.position, orientation);

        if (Input.GetKeyDown(KeyCode.T))
        {
            Attack();
        }
    }
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

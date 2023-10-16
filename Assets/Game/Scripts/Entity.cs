using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    #region Fields
    private float health;
    private float maximumHealth;
    #endregion

    #region Methods
    public abstract void Die();
    #endregion
}

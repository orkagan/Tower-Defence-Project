using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    #region Fields
    [SerializeField] protected float health;
    protected float maximumHealth;
    #endregion

    #region Methods
    public static void Die()
    { 
    
    }
    #endregion
}

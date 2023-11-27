using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour
{
    #region Fields
    [SerializeField] protected float health;
    protected float _maximumHealth;

    protected UnityEvent onDeath;
    #endregion

    #region Methods
    public virtual IEnumerator Die()
    {
        yield return null;
    }
    #endregion
}

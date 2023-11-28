using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour
{
    #region Fields
    [SerializeField] protected float health;
    protected float _maximumHealth = 100f;

    protected UnityEvent onDeath;
    #endregion

    public float GetHealth
    {
        get => health;
        set => health = value;
    }

    #region Methods
    public virtual IEnumerator Die()
    {
        yield return null;
    }
    #endregion
}

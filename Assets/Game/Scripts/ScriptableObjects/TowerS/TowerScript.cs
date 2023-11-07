using System;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    //scriptable object of the tower
    public Tower _towerSo;

    #region Properties
    /// <summary>
    /// The name of the Tower via ScriptableObject.
    /// </summary>
    public string GetName => _towerSo.entityName;
    /// <summary>
    /// The number of resources(int) required to place/upgrade a tower.
    /// </summary>
    public int GetCost => _towerSo.cost;
    /// <summary>
    /// The damage the tower deals towards enemies.
    /// </summary>
    public float GetDamage
    {
        get => _towerSo.damage;
        set => _towerSo.damage = value;
    }
    /// <summary>
    /// The number of seconds(float) between each tower's attack.
    /// </summary>
    public float GetAttackCooldown
    {
        get => _towerSo.attackCooldown;
        set => _towerSo.attackCooldown = value;
    }
    /// <summary>
    /// The distance around the tower - where the tower searches for enemies.
    /// </summary>
    public float GetRange
    {
        get => _towerSo.range;
        set => _towerSo.range = value;
    }
    #endregion

    private void OnValidate()
    {
        if (_towerSo != null && _towerSo.entityName != null)
        {
            gameObject.name = _towerSo.entityName;
        }
    }
}
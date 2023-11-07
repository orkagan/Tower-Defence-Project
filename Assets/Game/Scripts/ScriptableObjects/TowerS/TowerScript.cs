using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public Tower towerSO;

    public string GetName
    {
        get => towerSO.entityName;
    }
    
    public int GetCost
    {
        get => towerSO.cost;
    }

    private void OnValidate()
    {
        string towerName = gameObject.name;
        if (towerSO != null && towerSO.entityName != null)
        {
            gameObject.name = towerSO.entityName;
        }
        else
        {
            gameObject.name = towerName;
        }
    }
}

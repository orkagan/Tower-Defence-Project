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
}

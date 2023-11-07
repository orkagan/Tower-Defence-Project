using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public GameObject healthBar, resources, phaseText;

    public int GetResourceCount
    {
        get => resources.GetComponent<IncreaseDecreaseNumber>().GetCurrentCount;
    }
}

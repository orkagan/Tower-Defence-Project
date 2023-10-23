using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseDecreaseNumber : MonoBehaviour
{
    [SerializeField] private Text goString;
    private int number;

    private void Start()
    {
        number = 0;
    }

    public void IncreaseAmount(int amount)
    {
        number += amount;
        goString.text = number.ToString();
    }

    public void DecreaseAmount(int amount)
    {
        number -= amount;
        if (number <= 0)
        {
            number = 0;
        }
        
        goString.text = number.ToString();
    }
}

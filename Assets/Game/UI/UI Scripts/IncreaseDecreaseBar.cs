using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseDecreaseBar : MonoBehaviour
{
    public Image fillBar;

    public void IncreaseBar(float amount)
    {
        fillBar.fillAmount += amount;
        if (fillBar.fillAmount >= 1)
        {
            fillBar.fillAmount = 1;
        }
    }

    public void DecreaseBar(float amount)
    {
        fillBar.fillAmount -= amount;
        if (fillBar.fillAmount <= 0)
        {
            fillBar.fillAmount = 0;
        }
    }
}

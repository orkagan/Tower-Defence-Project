using UnityEngine;
using UnityEngine.UI;

public class IncreaseDecreaseNumber : MonoBehaviour
{
    [SerializeField] private Text goString;
    [SerializeField] private int number;

    public int GetCurrentCount
    {
        get => number;
    }

    private void Start()
    {
        goString.text = number.ToString();
    }

    public void IncreaseAmount(int amount)
    {
        number += amount;
        Start();
    }

    public void DecreaseAmount(int amount)
    {
        number -= amount;
        if (number <= 0)
        {
            number = 0;
        }
        
        Start();
    }
}

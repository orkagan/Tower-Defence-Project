using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleButtonInteraction : MonoBehaviour
{
    public UnityEvent OnButtonClick;
    
    public void ToggleButton()
    {
        GetComponent<Button>().interactable = !GetComponent<Button>().interactable;

        OnButtonClick.Invoke();
    }
}

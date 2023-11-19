using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardInputManager : MonoBehaviour
{
    public KeyboardInputs[] inputs;
    
    private void Update()
    {
        foreach (KeyboardInputs key in inputs)
        {
            KeyCode k = key.input;
            if (Input.GetKeyDown(k))
            {
                Debug.Log($"Key {key.name} was pressed.");
                key.onKeyDown.Invoke();
            }
        }
    }
}

[Serializable]
public struct KeyboardInputs
{
    public KeyCode input;
    public string name;
    public UnityEvent onKeyDown;
}
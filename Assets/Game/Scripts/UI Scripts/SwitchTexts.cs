using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTexts : MonoBehaviour
{
    [SerializeField] private Text _textObject;

    public void Update()
    {
        if (GameStateHandler.Instance.GetCurrentState == GameState.AttackPhase)
        {
            _textObject.text = "Attack Phase";
        }
        else if (GameStateHandler.Instance.GetCurrentState == GameState.BuildPhase)
        {
            _textObject.text = "Build Phase";
        }
    }
}

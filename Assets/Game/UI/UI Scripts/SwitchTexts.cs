using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GamePhases
{
    BuildPhase,
    AttackPhase,
}
public class SwitchTexts : MonoBehaviour
{
    [SerializeField] private Text _textObject;
    [SerializeField] private GamePhases _phase;

    public void SwitchText()
    {
        if (_phase == GamePhases.BuildPhase)
        {
            _textObject.text = "Attack Phase";
            _phase = GamePhases.AttackPhase;
        }
        else if (_phase == GamePhases.AttackPhase)
        {
            _textObject.text = "Build Phase";
            _phase = GamePhases.BuildPhase;
        }
    }
}

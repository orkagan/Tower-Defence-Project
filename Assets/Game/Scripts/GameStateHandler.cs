using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public enum GameState
{
    BuildPhase,
    AttackPhase
}

public class GameStateHandler : MonoBehaviour
{
    #region Singleton

    public static GameStateHandler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] GameState state;
    public UnityEvent onAttackPhase, onBuildPhase;

    public GameState GetCurrentState => state;

    public void SwitchState()
    {
        if (state == GameState.BuildPhase)
        {
            state = GameState.AttackPhase;
            onAttackPhase.Invoke();
        }
        else
        {
            state = GameState.BuildPhase;
            onBuildPhase.Invoke();
        }
    }
}
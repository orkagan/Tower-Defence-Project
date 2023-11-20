using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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
    [SerializeField] private NavMeshSurface _navMeshSurface;
    public UnityEvent onAttackPhase, onBuildPhase;

    public GameState GetCurrentState => state;

    public void SwitchState()
    {
        if (state == GameState.BuildPhase)
        {
            state = GameState.AttackPhase;
            _navMeshSurface.BuildNavMesh(); //rebuilds the navmesh map to include tower areas (high cost areas which enemies will try to avoid)
            onAttackPhase.Invoke();
        }
        else
        {
            state = GameState.BuildPhase;
            onBuildPhase.Invoke();
        }
    }
}
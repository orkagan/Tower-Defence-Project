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
    [SerializeField] private CanvasGroup _towerSelection;
    public UnityEvent onAttackPhase, onBuildPhase;

    public GameState GetCurrentState => state;

    /// <summary>
    /// Changes game state from Attack to Build, or Build to Attack.
    /// </summary>
    public void SwitchState()
    {
        if (state == GameState.BuildPhase)
        {
            state = GameState.AttackPhase;
            
            _towerSelection.interactable = false;
            _towerSelection.blocksRaycasts = false;
            _navMeshSurface.BuildNavMesh(); //rebuilds the navmesh map to include tower areas (high cost areas which enemies will try to avoid)
            
            onAttackPhase.Invoke();
        }
        else
        {
            state = GameState.BuildPhase;
            
            _towerSelection.interactable = true;
            _towerSelection.blocksRaycasts = true;
            
            onBuildPhase.Invoke();
        }
    }
}
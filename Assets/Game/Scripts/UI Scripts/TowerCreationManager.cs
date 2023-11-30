using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum PlayMode
{
    BuildMode,
    Other,
}

public class TowerCreationManager : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject[] _tower;
    [HideInInspector] public int chosenTower;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private PlayMode _playMode = PlayMode.BuildMode;
    [SerializeField] private GameObject _hud;

    InputMaster _controls;
    InputAction _click, _pos;
    #endregion

    #region Properties
    public PlayMode CurrentPlayMode => _playMode;
    private HUDManager hud => _hud.GetComponent<HUDManager>();
    #endregion

    #region Methods
    #region Unity Methods
    private void Awake()
    {
        _controls = new InputMaster();
        _click = _controls.FindAction("PlaceTower");
        _pos = _controls.FindAction("ClickPosition");
    }

    private void OnEnable()
    {
        _controls.Enable();
        _click.performed += CreateTower;
    }

    private void OnDisable()
    {
        _controls.Disable();
        _click.performed -= CreateTower;
    }
    #endregion

    private void CreateTower(InputAction.CallbackContext action)
    {
        if (GameStateHandler.Instance.GetCurrentState == GameState.BuildPhase) CreateTowerHere();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void CreateTowerHere()
    {
        //the following can only be executed when the player can build towers.
        if (_playMode == PlayMode.BuildMode)
        {
            //shoots a raycast from screen center to placement position via world space and places a tower
            //a tower is only placed if the player has enough resources/money to build one.
            Vector3 placement = _pos.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(placement);
            if (Physics.Raycast(ray, out RaycastHit rayHit, Mathf.Infinity, _layer))
            {
                //Check for nearby towers - if one is too close, display message saying a tower cannot be placed there for that reason.
                if (!CheckForTowers(rayHit.point))
                {
                    Tower t = _tower[chosenTower].GetComponentInChildren<Tower>();
                    int towerCost = t.GetCost;
                    int result = hud.GetResourceCount - towerCost;
                    //If a tower can be placed, refer to its attached ScriptableObject and the player HUD UI
                    //If the player does not have enough resources, it will not place.
                    if (result < 0)
                    {
                        Debug.Log("Player does not have enough resources.");
                    }
                    //If they do, remove from the player's resource count the cost of the tower to place. 
                    else
                    {
                        hud.SetResourceCount(towerCost, true);
                        Instantiate(_tower[chosenTower], rayHit.point, Quaternion.identity, transform);
                        Debug.Log($"{t.GetName} cost the player {t.GetCost} resources.");
                    }
                }
                else
                {
                    // display message
                    Debug.Log("This is too close to another tower.");
                }
            }
        }
    }

    //Checks for nearby towers using OverlapSphere, which creates a sphere from a Vector3 position, and returns every collider within it.
    //Then checks whether any of those colliders belongs to a tower. Returns true if there is, false if not.
    private bool CheckForTowers(Vector3 point)
    {
        Tower ts = _tower[chosenTower].GetComponentInChildren<Tower>();
        float towerDistance = ts.GetRange;

        Collider[] hitColliders = Physics.OverlapSphere(point, towerDistance);
        foreach (Collider col in hitColliders)
        {
            if (col.transform.TryGetComponent(out Tower tower))
            {
                tower.enabled = true;
                return true;
            }
        }
        return false;
    }

    public void SetChosenTower(int i) => chosenTower = i;

    public void SwitchToBuildMode(bool mode) => _playMode = mode ? PlayMode.BuildMode : PlayMode.Other;
    #endregion
}
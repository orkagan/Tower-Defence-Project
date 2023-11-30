using UnityEngine;
using UnityEngine.Events;

public enum PlayMode
{
    BuildMode,
    Other,
}

public class CreateTowerOnMouseClick : MonoBehaviour
{
    [SerializeField] private GameObject[] _tower;
    [HideInInspector] public int chosenTower = 0;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private PlayMode _playMode = PlayMode.BuildMode;
    [SerializeField] private GameObject _hud;
    
    public PlayMode CurrentPlayMode => _playMode;
    private HUDManager hud => _hud.GetComponent<HUDManager>();

    private void Update()
    {
        if (GameStateHandler.Instance.GetCurrentState == GameState.BuildPhase)
        {
            CreateTower();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void CreateTower()
    {
        //the following can only be executed when the player can build towers.
        if (_playMode == PlayMode.BuildMode && Input.GetMouseButtonDown(1))
        {
            //shoots a raycast from screen center to mouse position via world space and places a tower
            //a tower is only placed if the player has enough resources/money to build one.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
}
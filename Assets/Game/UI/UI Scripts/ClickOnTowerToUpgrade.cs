using UnityEngine;
using UnityEngine.Events;

public class ClickOnTowerToUpgrade : MonoBehaviour
{
    private CreateTowerOnMouseClick _createTowerScript;
    [SerializeField] private LayerMask _layer;
    public UnityEvent onTowerClick;

    private void Start()
    {
        _createTowerScript = GetComponent<CreateTowerOnMouseClick>();
    }

    private void Update()
    {
        ClickOnTower();
    }

    private void ClickOnTower()
    {
        if (_createTowerScript.CurrentPlayMode == PlayMode.Other)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit rayHit, Mathf.Infinity, _layer)
                && rayHit.collider.TryGetComponent(out TowerScript tower))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    tower.enabled = true;
                    onTowerClick.Invoke();
                }
            }
        }
    }
}
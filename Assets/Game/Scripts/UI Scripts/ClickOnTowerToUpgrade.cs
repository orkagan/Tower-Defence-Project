using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickOnTowerToUpgrade : MonoBehaviour
{
    private CreateTowerOnMouseClick _createTowerScript;
    [SerializeField] private Text _nameOfThingToUpgrade;
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
                && rayHit.collider.TryGetComponent(out TowerScript t))
            {
                TowerScript tower = rayHit.collider.GetComponent<TowerScript>();
                if (Input.GetMouseButtonDown(0))
                {
                    t.enabled = true;
                    _nameOfThingToUpgrade.text = $"Upgrade {tower.GetName}";
                    onTowerClick.Invoke();
                }
            }
        }
    }
}
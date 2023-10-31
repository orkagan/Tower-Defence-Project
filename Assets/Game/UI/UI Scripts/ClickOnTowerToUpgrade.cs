using UnityEngine;

public class ClickOnTowerToUpgrade : MonoBehaviour
{
    private CreateTowerOnMouseClick _createTowerScript;
    [SerializeField] private LayerMask _layer;

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
                tower.enabled = true;
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Yup, that's a tower.");
                }
            }
        }
    }
}
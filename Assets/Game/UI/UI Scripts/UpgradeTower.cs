using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTower : MonoBehaviour
{
    private CreateTowerOnMouseClick _createTowerScript;
    [SerializeField] private LayerMask _layer;

    private void Start()
    {
        _createTowerScript = GetComponent<CreateTowerOnMouseClick>();
    }

    private void Update()
    {
        if (_createTowerScript.CurrentPlayMode == PlayMode.Other)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit rayHit, Mathf.Infinity, _layer))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    
                }
            }
        }
    }
}

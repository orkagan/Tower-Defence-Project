using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CreateTowerOnMouseClick : MonoBehaviour
{
    [SerializeField] private GameObject _tower;
    [SerializeField] private LayerMask _layer;
    public UnityEvent onMouseClick;
    
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit rayHit, Mathf.Infinity, _layer))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(_tower, rayHit.point, Quaternion.identity, transform);
                onMouseClick.Invoke();
            }
        }
    }
}
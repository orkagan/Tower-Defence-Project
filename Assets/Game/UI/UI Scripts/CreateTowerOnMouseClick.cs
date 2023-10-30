using System;
using System.Collections;
using System.Collections.Generic;
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
    public UnityEvent onMouseClick;

    public PlayMode CurrentPlayMode
    {
        get => _playMode;
    }
    
    private void Update()
    {
        CreateTower();
    }

    private void CreateTower()
    {
        if (_playMode == PlayMode.BuildMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit rayHit, Mathf.Infinity, _layer))
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Instantiate(_tower[chosenTower], rayHit.point, Quaternion.identity, transform);
                    onMouseClick.Invoke();
                }
            }
        }
    }

    public void SetChosenTower(int i)
    {
        chosenTower = i;
    }

    public void SwitchToBuildMode(bool mode)
    {
        _playMode = mode ? PlayMode.BuildMode : PlayMode.Other;
    }
}
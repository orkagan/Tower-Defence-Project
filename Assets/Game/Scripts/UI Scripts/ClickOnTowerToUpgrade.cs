using System;
using UnityEngine;
using UnityEngine.UI;

public class ClickOnTowerToUpgrade : MonoBehaviour
{
    [SerializeField] private Text _nameOfThingToUpgrade;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Text _rangeText, _cooldownText, _damageText;
    [SerializeField] private Button _rangeBtn, _cooldownBtn, _damageBtn;

    public GameObject upgradePanel;

    private Tower _tower;

    private CreateTowerOnMouseClick CreateTower => GetComponent<CreateTowerOnMouseClick>();

    private void Update()
    {
        if (CreateTower.CurrentPlayMode == PlayMode.Other)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit rayHit, Mathf.Infinity, _layer)
                    && rayHit.collider.TryGetComponent(out Tower t))
                {
                    _tower = rayHit.collider.GetComponent<Tower>();
                    
                    ClickOnTower(t);
                }
            }
        }
    }

    //whenever the player clicks on a tower
    private void ClickOnTower(Tower t)
    {
        t.enabled = true;
        _nameOfThingToUpgrade.text = $"Upgrade {_tower.GetName}";

        _rangeBtn.onClick.RemoveAllListeners();
        _cooldownBtn.onClick.RemoveAllListeners();
        _damageBtn.onClick.RemoveAllListeners();

        _rangeBtn.onClick.AddListener(() => _tower.SetRange(1));
        _cooldownBtn.onClick.AddListener(() => _tower.SetAttackCooldown(1));
        _damageBtn.onClick.AddListener(() => _tower.SetDamage(1));
        
        UpdateDisplay();
        upgradePanel.SetActive(true);
    }

    private void UpdateDisplay()
    {
        _rangeText.text = _tower.GetRange.ToString();
        _cooldownText.text = _tower.GetAttackCooldown.ToString();
        _damageText.text = _tower.GetDamage.ToString();
    }
}
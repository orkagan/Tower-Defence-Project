using UnityEngine;
using UnityEngine.UI;

public class ClickOnTowerToUpgrade : MonoBehaviour
{
    [SerializeField] private Text _nameOfThingToUpgrade;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Text _rangeText, _cooldownText, _damageText;
    [SerializeField] private Button _rangeBtn, _cooldownBtn, _damageBtn;

    public GameObject upgradePanel;

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
                    ClickOnTower(t);
                }
            }
        }
    }

    //whenever the player clicks on a tower
    private void ClickOnTower(Tower t)
    {
        t.enabled = true;
        _nameOfThingToUpgrade.text = $"Upgrade {t.GetName}";

        _rangeBtn.onClick.RemoveAllListeners();
        _cooldownBtn.onClick.RemoveAllListeners();
        _damageBtn.onClick.RemoveAllListeners();

        _rangeBtn.onClick.AddListener(() => t.SetRange(1));
        _rangeBtn.onClick.AddListener(() => UpdateDisplay(t));
        
        _cooldownBtn.onClick.AddListener(() => t.SetAttackCooldown(1));
        _cooldownBtn.onClick.AddListener(() => UpdateDisplay(t));
        
        _damageBtn.onClick.AddListener(() => t.SetDamage(1));
        _damageBtn.onClick.AddListener(() => UpdateDisplay(t));
        
        upgradePanel.SetActive(true);
        UpdateDisplay(t);
    }

    private void UpdateDisplay(Tower t)
    {
        _rangeText.text = t.GetRange.ToString();
        _cooldownText.text = t.GetAttackCooldown.ToString();
        _damageText.text = t.GetDamage.ToString();
    }
}
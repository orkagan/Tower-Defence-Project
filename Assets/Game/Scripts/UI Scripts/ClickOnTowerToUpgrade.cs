using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ClickOnTowerToUpgrade : MonoBehaviour
{
    #region Fields
    [SerializeField] private Text _nameOfThingToUpgrade;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Text _rangeText, _cooldownText, _damageText;
    [SerializeField] private Button _rangeBtn, _cooldownBtn, _damageBtn;

    public GameObject upgradePanel;

    InputMaster _controls;
    InputAction _click, _pos;
    #endregion

    #region Properties
    private TowerCreationManager TowerCreator => GetComponent<TowerCreationManager>();
    #endregion

    #region Methods
    #region Unity Methods
    private void Awake()
    {
        _controls = new InputMaster();
        _click = _controls.FindAction("InteractTower");
        _pos = _controls.FindAction("ClickPosition");
    }

    private void OnEnable()
    {
        _controls.Enable();
        _click.performed += ClickOnTower;
    }

    private void OnDisable()
    {
        _controls.Disable();
        _click.performed -= ClickOnTower;
    }
    #endregion

    private void ClickOnTower(InputAction.CallbackContext action)
    {
        OnTowerClicked();
    }

    //whenever the player clicks on a tower
    private void OnTowerClicked()
    {
        if (TowerCreator.CurrentPlayMode != PlayMode.Other) return;

        Vector3 interaction = _pos.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(interaction);
        if (Physics.Raycast(ray, out RaycastHit rayHit, Mathf.Infinity, _layer)
            && rayHit.collider.TryGetComponent(out Tower t))
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
    }

    private void UpdateDisplay(Tower t)
    {
        _rangeText.text = t.GetRange.ToString();
        _cooldownText.text = t.GetAttackCooldown.ToString();
        _damageText.text = t.GetDamage.ToString();
    }
    #endregion
}
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TowerUpgrades : MonoBehaviour
{
    [Serializable]
    #region Upgrade Struct
    public struct Upgrade
    {
        /// <summary>
        /// The name of the upgrade which relates to a tower's attribute of the same name.
        /// </summary>
        public string upgradeName;
        /// <summary>
        /// The text
        /// </summary>
        private Text upgradeLevelText;
        private int upgradeLevelAmount;
        public UnityEvent onButtonPress;

        public string upText
        {
            get { return upgradeLevelText.text; }
            set { upgradeLevelText.text = value; }
        }

        public int upLevel
        {
            get { return upgradeLevelAmount; }
            set { upgradeLevelAmount = value; }
        }
    }
    #endregion

    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _upgradeInstancePrefab;
    [SerializeField] private Upgrade[] _upgradeAspect;
    public UnityEvent onSuccessfulUpgrade;

    private void Start()
    {
        foreach (Upgrade instance in _upgradeAspect)
        {
            GameObject upgrade = Instantiate(_upgradeInstancePrefab, _parent);
            UpgradeInstance _instance = upgrade.GetComponent<UpgradeInstance>();
            _instance.upgradeButtonText = instance.upgradeName;
            //_instance.upgradeText = instance.upText;
        }
    }
}
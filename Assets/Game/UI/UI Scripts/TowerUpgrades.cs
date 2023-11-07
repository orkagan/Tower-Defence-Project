using System;
using UnityEngine;
using UnityEngine.Events;

public class TowerUpgrades : MonoBehaviour
{
    [Serializable]
    public struct Upgrade
    {
        public string upgradeName;
        public UnityEvent onButtonPress;
    }

    [SerializeField] private GameObject _upgradeInstancePrefab;
    [SerializeField] private Upgrade[] _instanceOfUpgrade;

    private void Start()
    {
        foreach (Upgrade instance in _instanceOfUpgrade)
        {
            GameObject upgrade = Instantiate(_upgradeInstancePrefab, transform);
            UpgradeInstance _instance = upgrade.GetComponent<UpgradeInstance>();
            _instance.upgradeButtonText = instance.upgradeName;
            //on Upgrade button press, increase upgrade level by 1
        }
    }
}
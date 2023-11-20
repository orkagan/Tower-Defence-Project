using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeInstance : MonoBehaviour
{
    /// <summary>
    /// The UI Button GameObject attached to the upgrade instance.
    /// </summary>
    [SerializeField] Button upgradeButton;
    /// <summary>
    /// The UI Text GameObject attached to the upgrade instance.
    /// </summary>
    [SerializeField] Text upgradeLevel;

    /// <summary>
    /// The text of the UpgradeButton Button.
    /// </summary>
    public string upgradeButtonText
    {
        set => upgradeButton.GetComponentInChildren<Text>().text = value;
    }

    /// <summary>
    /// The text of the UpgradeLevel Text.
    /// </summary>
    public string upgradeText
    {
        set => upgradeLevel.text = value;
    }

    public UnityEvent onButtonPress => upgradeButton.onClick;
}

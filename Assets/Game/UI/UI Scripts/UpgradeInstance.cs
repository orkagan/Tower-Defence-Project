using UnityEngine;
using UnityEngine.UI;

public class UpgradeInstance : MonoBehaviour
{
    public Button upgradeButton;
    public Text upgradeLevelText;

    public string upgradeButtonText
    {
        get => upgradeButton.GetComponentInChildren<Text>().text;
        set => upgradeButton.GetComponentInChildren<Text>().text = value;
    }
}

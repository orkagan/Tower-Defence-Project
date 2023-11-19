using UnityEngine;

public class SwitchUIBasedOnDevice : MonoBehaviour
{
    public GameObject pCControls, mobileControls;

    private void OnEnable()
    {

        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            pCControls.SetActive(true);
            Debug.Log("This is a PC build.");
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            mobileControls.SetActive(true);
            Debug.Log("This is an Android build");
        }
    }
}

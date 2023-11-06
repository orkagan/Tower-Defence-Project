using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchUIBasedOnDevice : MonoBehaviour
{
    public GameObject pCControls, mobileControls;

    private void SwitchUI()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Debug.Log("This is a PC build.");
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            Debug.Log("This is an Android build");
        }
    }
}

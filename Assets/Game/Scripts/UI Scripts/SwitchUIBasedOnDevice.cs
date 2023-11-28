using System;
using UnityEngine;

public class SwitchUIBasedOnDevice : MonoBehaviour
{
    public GameObject pCControls, mobileControls;

    private void Start()
    {
        pCControls.SetActive(false);
        mobileControls.SetActive(false);
        
        SwitchUI();
        
#if UNITY_EDITOR
        pCControls.SetActive(true);
#endif
        
    }

    private void SwitchUI()
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

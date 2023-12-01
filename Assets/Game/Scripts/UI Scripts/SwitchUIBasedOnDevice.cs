using System;
using UnityEngine;

public class SwitchUIBasedOnDevice : MonoBehaviour
{
    public GameObject pCControls, mobileControls;

    public PlayerMovement pm => GameObject.FindObjectOfType<PlayerMovement>();

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
            pm.mobileMovement = false;
            Debug.Log("This is a PC build.");
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            mobileControls.SetActive(true);
            pm.mobileMovement = true;
            Debug.Log("This is an Android build");
        }
    }
}

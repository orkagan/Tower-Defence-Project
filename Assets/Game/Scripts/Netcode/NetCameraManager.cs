using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetCameraManager : MonoBehaviour
{
    public GameObject defaultCamera;
    public GameObject playerCamera;
    private void Start()
    {
        defaultCamera = Camera.main.gameObject;
        ChooseCamera();
    }

    [ContextMenu("ChooseCamera()")]
    public void ChooseCamera()
    {
		if (playerCamera != null)
		{
            playerCamera.SetActive(true);
            defaultCamera.SetActive(false);
        }
        else
		{
            defaultCamera.SetActive(true);
		}
    }

}
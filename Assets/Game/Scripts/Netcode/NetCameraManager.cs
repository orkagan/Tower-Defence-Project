using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetCameraManager : NetworkBehaviour
{
    public static Camera defaultCamera;
    private void Start()
    {
        defaultCamera = Camera.main;
    }

    [ContextMenu("ChooseCamera()")]
    public static void ChooseCamera()
    {
        if (NetworkManager.Singleton.ConnectedClientsList.Count == 0)
        {
            defaultCamera.gameObject.SetActive(true);
        }
        
        foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
        {
            GameObject cameraGO = client.PlayerObject.GetComponentInChildren<Camera>().gameObject;
            if (client.PlayerObject.IsLocalPlayer)
            {
                cameraGO.SetActive(true);
                defaultCamera.gameObject.SetActive(false);  
            }
            else
            {
                cameraGO.SetActive(false);
            }
        }
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetCameraManager : NetworkBehaviour
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
        if (NetworkManager.Singleton.ConnectedClientsList.Count == 0)
        {
            defaultCamera.gameObject.SetActive(true);
        }

        foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClients.Values)
        {
            GameObject cameraGO = client.PlayerObject.GetComponentInChildren<Camera>().gameObject;
            if (client.PlayerObject.IsLocalPlayer)
            {
                cameraGO.SetActive(true);
                playerCamera = cameraGO;
                defaultCamera.SetActive(false);
            }
            else
            {
                cameraGO.SetActive(false);
            }
        }
    }

}
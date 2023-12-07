using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetPlayerHandler : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        HUDManager hud = GetComponentInChildren<HUDManager>();
        if (IsLocalPlayer)
        {
            hud.gameObject.SetActive(true);
        }
        else
        {
            hud.gameObject.SetActive(false);
        }
        
        if (!IsOwner)
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Player>().enabled = false;
            GetComponentInChildren<CameraHandler>().enabled = false;
            GetComponentInChildren<Camera>().gameObject.SetActive(false);
        }
        GameObject.FindObjectOfType<NetCameraManager>().ChooseCamera();
    }
    public override void OnNetworkDespawn()
    {
        GameObject.FindObjectOfType<NetCameraManager>().ChooseCamera();
    }
}
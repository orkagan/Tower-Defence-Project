using System;
using Unity.Netcode;
using UnityEngine;
public class NetCameraHandler : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            GetComponent<CameraHandler>().enabled = false;
            gameObject.SetActive(false);
            return;
        }
        if (IsOwner)
        {
            GameObject.FindObjectOfType<NetCameraManager>().playerCamera = gameObject;
            GameObject.FindObjectOfType<NetCameraManager>().ChooseCamera();
        }
    }

	public override void OnNetworkDespawn()
	{
        if (IsOwner)
        {
            GameObject.FindObjectOfType<NetCameraManager>().playerCamera = null;
            GameObject.FindObjectOfType<NetCameraManager>().ChooseCamera();
        }
    }
}

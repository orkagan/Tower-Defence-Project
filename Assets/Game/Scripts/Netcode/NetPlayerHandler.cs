using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetPlayerHandler : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            GetComponentInChildren<CameraHandler>().enabled = false;
            GetComponentInChildren<Camera>().gameObject.SetActive(false);
            GetComponent<PlayerMovement>().enabled = false;
        }
        NetCameraManager.ChooseCamera();
    }
}

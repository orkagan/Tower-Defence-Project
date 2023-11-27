using System;
using Unity.Netcode;
using UnityEngine;
public class NetCameraHandler : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) GetComponent<CameraHandler>().enabled = false;
        NetCameraManager.ChooseCamera();
    }
}

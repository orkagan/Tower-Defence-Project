using Unity.Netcode;
using UnityEngine;

public class NetPlayerMovement : NetworkBehaviour
{
    public void OnNetworkSpawned()
    {
        if (!IsOwner) GetComponent<PlayerMovement>().enabled = false;
    }
}

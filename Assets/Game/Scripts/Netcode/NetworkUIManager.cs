using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class NetworkUIManager : MonoBehaviour
{
    [SerializeField] private InputField _ipField;
    [SerializeField] private InputField _portField;
    //[SerializeField] private Button _serverButton;
    [SerializeField] private Button _hostButton;
    [SerializeField] private Button _joinButton;
    [SerializeField] private Button _disconnectButton;

    [SerializeField] private string defaultIP = "127.0.0.1";
    [SerializeField] private int defaultPort = 7777;

    private void Awake()
    {
        //_serverButton.onClick.AddListener(()=>{NetworkManager.Singleton.StartServer();});
        _hostButton.onClick.AddListener(() => { HostWithIP(); });
        _joinButton.onClick.AddListener(() => { JoinWithIP(); });
        _disconnectButton.onClick.AddListener(() => { Disconnect(); });

        NetworkManager.Singleton.OnServerStarted += () => { _disconnectButton.interactable = true; };
        NetworkManager.Singleton.OnClientStarted += () => { _disconnectButton.interactable = true; };
    }
    public void SetConnectionData(string ip = "127.0.0.1", string port = "7777")
    {
        int.TryParse(port, out var portNum);
        if (portNum <= 0 || string.IsNullOrEmpty(port))
        {
            portNum = defaultPort;
        }

        ip = string.IsNullOrEmpty(ip) ? defaultIP : ip;
        Debug.Log($"Connection Data Set as {ip}:{portNum}");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
            ip,
            (ushort)portNum
            );
    }
    public void HostWithIP()
    {
        SetConnectionData(_ipField.text, _portField.text);
        NetworkManager.Singleton.StartHost();
    }

    public void JoinWithIP()
    {
        SetConnectionData(_ipField.text, _portField.text);
        NetworkManager.Singleton.StartClient();
    }

    public void Disconnect()
    {
        NetworkManager.Singleton.Shutdown();
        _disconnectButton.interactable = false;
    }
    
}

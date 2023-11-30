using System;
using System.Linq;
using System.Net;
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

    //[SerializeField] private string defaultIP = "127.0.0.1";
    [SerializeField] private string defaultIP;
    [SerializeField] private int defaultPort = 7777;
    
    public event Action<ulong, ConnectionStatus> OnClientConnectionNotification;
    public event Action<bool> OnServerStoppedNotification;
    public enum ConnectionStatus
    {
        Connected,
        Disconnected
    }
    
    
    private void Awake()
    {
        defaultIP = GetLocalIPv4();
        
        //_serverButton.onClick.AddListener(()=>{NetworkManager.Singleton.StartServer();});
        _hostButton.onClick.AddListener(() => { HostWithIP(); });
        _joinButton.onClick.AddListener(() => { JoinWithIP(); });
        _disconnectButton.onClick.AddListener(() => { Disconnect(); });
    }

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
        NetworkManager.Singleton.OnServerStopped += OnServerStopped;
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tilde))
        {
            Debug.developerConsoleVisible = !Debug.developerConsoleVisible;
        }
    }*/

    public string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName())
            .AddressList.First(
                f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            .ToString();
    }
    public void SetConnectionData(string ip = "127.0.0.1", string port = "7777")
    {
        int.TryParse(port, out var portNum);
        if (portNum <= 0 || string.IsNullOrEmpty(port))
        {
            portNum = defaultPort;
        }

        ip = string.IsNullOrEmpty(ip) ? defaultIP : ip;
        IPAddress address;
        if (IPAddress.TryParse(ip, out address))
        {
            switch (address.AddressFamily)
            {
                case System.Net.Sockets.AddressFamily.InterNetwork:
                    // we have IPv4
                    break;
                case System.Net.Sockets.AddressFamily.InterNetworkV6:
                    // we have IPv6
                    break;
                default:
                    // umm... yeah... I'm going to need to take your red packet and...
                    ip = GetLocalIPv4();
                    break;
            }
        }
        
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
    }
    
    private void OnClientConnectedCallback(ulong clientId)
    {
        OnClientConnectionNotification?.Invoke(clientId, ConnectionStatus.Connected);
        _ipField.text = NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address;
        _portField.text = NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Port.ToString();
        _disconnectButton.interactable = true;
        _joinButton.interactable = false;
        _hostButton.interactable = false;
    }

    private void OnClientDisconnectCallback(ulong clientId)
    {
        OnClientConnectionNotification?.Invoke(clientId, ConnectionStatus.Disconnected);
        _disconnectButton.interactable = false;
        _joinButton.interactable = true;
        _hostButton.interactable = true;
    }
    private void OnServerStopped(bool dced)
    {
        OnServerStoppedNotification?.Invoke(dced);
        _disconnectButton.interactable = false;
        _joinButton.interactable = true;
        _hostButton.interactable = true;
    }
}

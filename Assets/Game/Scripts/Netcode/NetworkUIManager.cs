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

    [SerializeField] private string defaultIP;
    [SerializeField] private int defaultPort = 7777;
    
    
    private void Awake()
    {
        //sets default ip to local IPv4 (used if blank)
        defaultIP = GetLocalIPv4();
        
        //_serverButton.onClick.AddListener(()=>{NetworkManager.Singleton.StartServer();});
        _hostButton.onClick.AddListener(() => { HostWithIP(); });
        _joinButton.onClick.AddListener(() => { JoinWithIP(); });
        _disconnectButton.onClick.AddListener(() => { Disconnect(); });
    }

    private void Start()
    {
        //Listener functions
        NetworkManager.Singleton.OnClientStarted += OnClientStarted;
        NetworkManager.Singleton.OnClientStopped += OnClientStopped;
        NetworkManager.Singleton.OnServerStopped += OnServerStopped;
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
    }

    public string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName())
            .AddressList.First(
                f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            .ToString();
    }
    public void SetConnectionData(string ip, string port = "7777")
    {
        //Port checking
        int.TryParse(port, out var portNum);
        if (portNum <= 0 || string.IsNullOrEmpty(port))
        {
            portNum = defaultPort;
        }

        //IP checking
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
                    // grabbing IPv4 address 
                    ip = GetLocalIPv4();
                    break;
            }
        }
        
        Debug.Log($"Connection Data Set as {ip}:{portNum}");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
            ip,
            (ushort)portNum);
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
    
    //I was hoping to just get funtions to listen to network events like connected, disconnected, etc.
    #region Listener Functions
    private void OnClientStarted()
    {
        _ipField.text = NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address;
        _portField.text = NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Port.ToString();
        _ipField.interactable = false;
        _portField.interactable = false;

        //set interactivity of buttons
        _disconnectButton.interactable = true;
        _joinButton.interactable = false;
        _hostButton.interactable = false;
    }

    private void OnClientConnectedCallback(ulong clientId)
    {
        _ipField.interactable = false;
        _portField.interactable = false;

        //set interactivity of buttons
        _disconnectButton.interactable = true;
        _joinButton.interactable = false;
        _hostButton.interactable = false;
    }

    private void OnClientDisconnectCallback(ulong clientId)
    {
        _ipField.interactable = true;
        _portField.interactable = true;

        //set interactivity of buttons
        _disconnectButton.interactable = false;
        _joinButton.interactable = true;
        _hostButton.interactable = true;
    }
    private void OnClientStopped(bool dced)
    {
        _ipField.interactable = true;
        _portField.interactable = true;

        //set interactivity of buttons
        _disconnectButton.interactable = false;
        _joinButton.interactable = true;
        _hostButton.interactable = true;
    }
    private void OnServerStopped(bool dced)
    {
        _ipField.interactable = true;
        _portField.interactable = true;

        //set interactivity of buttons
        _disconnectButton.interactable = false;
        _joinButton.interactable = true;
        _hostButton.interactable = true;
    }
    #endregion
}

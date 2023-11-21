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
    
    public string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName())
            .AddressList.First(
                f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            .ToString();
    }
    
    private void Awake()
    {
        defaultIP = GetLocalIPv4();
        
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
                    ip = defaultIP;
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
        _disconnectButton.interactable = false;
    }
    
}

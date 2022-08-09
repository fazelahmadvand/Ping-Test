using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Net;
using kcp2k;
using System;
using System.Linq;

public class Manager : Singlton<Manager>
{

    [SerializeField] private InGameNetworkManager network;
    [SerializeField] private KcpTransport kcp;
    [SerializeField] private bool runAsServer;

    [Space]
    [Header("Server Info")]
    [SerializeField] private string serverAddress;
    [SerializeField] private ushort serverPort;

    public InGameNetworkManager NetworkManager => network;

    public bool RunAsServer => runAsServer;

    [SerializeField] private string[] args = new string[10];


    private void Start()
    {
        GetArgs();
        if (RunAsServer)
        {
#if !UNITY_EDITOR
            StartServer();
#elif UNITY_EDITOR
            network.StartServer();
            Debug.Log("Server Address=>" + network.networkAddress);
            Debug.Log("Server Port=>" + kcp.Port);
#endif
        }
    }

    private void Update()
    {
        HUDManager.Instance.ShowPing((NetworkTime.rtt * 1000).ToString("F0"));
    }
    public void StartServer()
    {
        GetArgs();
        network.networkAddress = serverAddress;
        kcp.Port = serverPort;

        network.StartServer();

        Debug.Log("Start Server At Port=>" + kcp.Port);
        Debug.Log("Start Server At Address=>" + network.networkAddress);

    }

    public string GetServerIP()
    {
        string hostName = Dns.GetHostName();

        string IP = Dns.GetHostEntry(hostName).AddressList[1].ToString();

        if (string.IsNullOrEmpty(IP))
            return string.Empty;
        return IP;
    }

    public void StartClient(string address, ushort port)
    {
        network.networkAddress = address;
        kcp.Port = port;
        network.StartClient();
    }


    private void GetArgs()
    {
        var inputs = Environment.GetCommandLineArgs();
        args = inputs;

#if !UNITY_EDITOR
        if (args.Length > 0)
        {
            serverAddress = args[1];
            serverPort = ushort.Parse(args[2]);
        }
#endif


    }




}

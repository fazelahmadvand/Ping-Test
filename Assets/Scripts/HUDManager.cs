using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : Singlton<HUDManager>
{

    [SerializeField] private GameObject connectHelperRoot;
    [SerializeField] private Text machineIP;

    [Space]
    [Header("Server")]
    [SerializeField] private Button serverOnlyBtn;

    [Space]
    [Header("Host")]
    [SerializeField] private Button hostBtn;


    [Space]
    [Header("Client")]
    [SerializeField] private Button localClientBtn;

    [SerializeField] private InputField serverAddressTxt;
    [SerializeField] private InputField serverPortTxt;
    [SerializeField] private Button clientBtn;

    [Space]
    [Header("Game Info")]
    [SerializeField] private Text pingTxt;


    private void Start()
    {
        Hide();
        if (Manager.Instance.RunAsServer) return;
        Show();
        machineIP.text = "Machine IPv4 Address: " + Manager.Instance.GetServerIP();

        serverOnlyBtn.onClick.AddListener(() =>
        {
            Manager.Instance.NetworkManager.StartServer();
            Hide();
        });

        hostBtn.onClick.AddListener(() =>
        {
            Manager.Instance.NetworkManager.StartHost();
            Hide();
        });


        localClientBtn.onClick.AddListener(() =>
        {
            Manager.Instance.NetworkManager.StartClient();
            Hide();
        });

        clientBtn.onClick.AddListener(() =>
        {
            string address = serverAddressTxt.text;
            string portStr = serverPortTxt.text;
            ushort port = ushort.Parse(portStr);
            Manager.Instance.StartClient(address, port);
            Hide();

        });




    }


    private void Show() => connectHelperRoot.SetActive(true);
    private void Hide() => connectHelperRoot.SetActive(false);

    public void ShowPing(string ping)
    {
        pingTxt.text = "Ping: " + ping;
    }

}

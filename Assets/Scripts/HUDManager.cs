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
    [Header("CLient")]
    [SerializeField] private InputField serverAddressTxt;
    [SerializeField] private InputField serverPortTxt;
    [SerializeField] private Button clientBtn;





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

        clientBtn.onClick.AddListener(() =>
        {
            string address = serverAddressTxt.text;
            string portStr = serverPortTxt.text;
            ushort port = ushort.Parse(portStr);
            Manager.Instance.StartClient(address, port);

        });




    }


    private void Show() => connectHelperRoot.SetActive(true);
    private void Hide() => connectHelperRoot.SetActive(false);



}

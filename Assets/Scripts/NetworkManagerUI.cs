#region

using System;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class NetworkManagerUI : NetworkBehaviour
{
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    [SerializeField] private TMP_Text statusText;

    private NetworkManager netManager;

    private string gameType;

    private void Awake()
    {
        netManager = NetworkManager.Singleton;

        serverBtn.onClick.AddListener(selectServer);

        hostBtn.onClick.AddListener(selectHost);

        clientBtn.onClick.AddListener(selectClient);
    }

    private void selectServer()
    {
        gameType = "SERVER";
        netManager.StartServer();
    }

    private void selectHost()
    {
        gameType = "HOST";
        netManager.StartHost();
    }

    private void selectClient()
    {
        gameType = "CLIENT";
        netManager.StartClient();
    }

    public override void OnNetworkSpawn()
    {
        statusText.text = gameType + " | ID: " + OwnerClientId;
    }

    private void Start()
    {
        if (Application.isEditor) return;

        Dictionary<string, string> args = GetCommandlineArgs();

        if (!args.TryGetValue("-mlapi", out string mlapiValue))
            return;

        switch (mlapiValue)
        {
            case "server":
                selectServer();
                break;
            case "host":
                selectHost();
                break;
            case "client":
                selectClient();
                break;
        }
    }

    private Dictionary<string, string> GetCommandlineArgs()
    {
        Dictionary<string, string> argDictionary = new();

        string[] args = Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; ++i)
        {
            string arg = args[i].ToLower();
            if (!arg.StartsWith("-"))
                continue;

            string value = i < args.Length - 1 ? args[i + 1].ToLower() : null;

            value = value?.StartsWith("-") ?? false ? null : value;

            argDictionary.Add(arg, value);
        }

        return argDictionary;
    }
}
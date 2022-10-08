using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private TMP_Text playerName;

    public override void OnNetworkSpawn()
    {
        playerName.text = OwnerClientId.ToString();
    }
}

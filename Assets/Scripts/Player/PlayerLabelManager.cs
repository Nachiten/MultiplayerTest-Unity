#region

using TMPro;
using Unity.Netcode;
using UnityEngine;

#endregion

public class PlayerLabelManager : NetworkBehaviour
{
    [SerializeField] private TMP_Text playerName;

    public void setPlayerName(string newPlayerName)
    {
        playerName.text = newPlayerName;
        
        // Only see the nametag of others, not yours
        if (IsOwner)
            playerName.gameObject.SetActive(false);
    }
}
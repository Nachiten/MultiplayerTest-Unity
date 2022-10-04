using System;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class CapsuleNetwork : NetworkBehaviour
{
    private NetworkList<PlayerSkin> playersSelectedSkins;

    private void Awake()
    {
        Debug.Log("[SETUP] Setting up NetworkList");
        playersSelectedSkins = new NetworkList<PlayerSkin>();
    }

    public struct PlayerSkin : INetworkSerializable, IEquatable<PlayerSkin>
    {
        public ulong clientId;
        public int skinIndex;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref clientId);
            serializer.SerializeValue(ref skinIndex);
        }

        public bool Equals(PlayerSkin other)
        {
            return clientId == other.clientId && 
                   skinIndex == other.skinIndex;
        }
    }

    public override void OnNetworkSpawn()
    {
        ulong clientId = OwnerClientId;
        Debug.Log("[OnNetworkSpawn] ClientID: " + clientId);
        
        addPlayerSkinServerRpc(clientId);
       
        updateSkin();
    }

    // public override void OnNetworkDespawn()
    // {
    //     // playersSelectedSkins.OnListChanged -= OnStateChanged;
    // }
    
    // public void OnStateChanged(NetworkListEvent<PlayerSkin> value)
    // {
    //     // Debug.Log("[OnStateChanged]: ClientId: " + OwnerClientId);
    //     //
    //     // updateSkin();
    // }

    private void updateSkin()
    {
        Debug.Log("[UpdateSkin]: ClientId: " + OwnerClientId);
        
        // Apply the value with our id
        foreach (PlayerSkin playerSkin in playersSelectedSkins)
        {
            if (playerSkin.clientId != OwnerClientId) 
                continue;

            GetComponent<MeshRenderer>().material = PlayerSkins.Instance.skins.ElementAt(playerSkin.skinIndex);
            Debug.Log("[UpdateSkin] My id is " + playerSkin.clientId + " | Applied skin index " + playerSkin.skinIndex);
            break;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void addPlayerSkinServerRpc(ulong clientId)
    {
        Debug.Log("[AddPlayerSkinServerRpc]: ClientId: " + clientId);
        
        //If there is already a skin for this client, don't add it again
        foreach (PlayerSkin playerSkin in playersSelectedSkins)
        {
            if (playerSkin.clientId == clientId) 
                return;
        }

        int skinIndex = PlayerSkins.Instance.selectRandomSkinIndex(playersSelectedSkins);
        
        //Debug.Log("[AddPlayerSkinServerRpc]: Adding new player skin: ClientId: " + clientId + " | SkinIndex: " + skinIndex);
        
        playersSelectedSkins.Add(new PlayerSkin
        {
            clientId = clientId,
            skinIndex = skinIndex
        });
    }
}

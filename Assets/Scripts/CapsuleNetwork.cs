#region

using TMPro;
using Unity.Netcode;
using UnityEngine;

#endregion

public class CapsuleNetwork : NetworkBehaviour
{
    [SerializeField] private TMP_Text playerIDText;

    private readonly NetworkVariable<ulong> clientId = new(0, NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    private readonly NetworkVariable<int> skinIndex = new(0, NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        Debug.Log("[OnNetworkSpawn] ClientId: " + OwnerClientId);

        skinIndex.OnValueChanged += OnSkinIndexChange;
        clientId.OnValueChanged += OnClientIdChange;

        if (IsOwner)
            setup();

        applyClientId();
        applySkinIndex();
    }

    public override void OnNetworkDespawn()
    {
        skinIndex.OnValueChanged -= OnSkinIndexChange;
        clientId.OnValueChanged -= OnClientIdChange;
    }

    private void setup()
    {
        clientId.Value = OwnerClientId;

        getPlayerSkinServerRpc(OwnerClientId);
    }

    private void OnClientIdChange(ulong oldClientId, ulong newClientId)
    {
        applyClientId();
    }

    private void OnSkinIndexChange(int oldIndex, int newIndex)
    {
        applySkinIndex();
    }

    private void applyClientId()
    {
        Debug.Log("[ApplyClientId] ClientId: " + clientId.Value);
        // Show client id on the text
        playerIDText.text = clientId.Value.ToString();
    }

    private void applySkinIndex()
    {
        Debug.Log("[ApplySkinIndex] SkinIndex: " + skinIndex.Value);
        // Apply the capsule material
        GetComponent<MeshRenderer>().material = PlayerSkins.Instance.getSkin(skinIndex.Value);
    }

    // Get random skin from server
    [ServerRpc]
    private void getPlayerSkinServerRpc(ulong theClientId)
    {
        Debug.Log("[GetPlayerSkinServerRpc] ClientId: " + theClientId);

        // Get assigned skin index
        int theSkinIndex = PlayerSkins.Instance.getRandomSkinIndex();

        // Client rpc is only called for the client who requested it
        ClientRpcParams clientRpcParams = new()
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new[] {theClientId}
            }
        };

        // Client now applies the skin
        applySkinClientRpc(theSkinIndex, clientRpcParams);
    }

    [ClientRpc]
    private void applySkinClientRpc(int newSkinIndex, ClientRpcParams clientRpcParams = default)
    {
        Debug.Log("[ApplySkinClientRpc] ClientId: " + OwnerClientId + " | SkinIndex: " + skinIndex.Value);

        // Set the skin index
        skinIndex.Value = newSkinIndex;
    }
}
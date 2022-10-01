using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CapsuleNetwork : NetworkBehaviour
{
    [SerializeField] private List<Material> playerSkins;

    private NetworkVariable<int> skinIndex = new(-1);
    
    public override void OnNetworkSpawn()
    {
        skinIndex.OnValueChanged += OnStateChanged;

        if (!IsOwner)
            return;
        
        int randomIndex = Random.Range(0, playerSkins.Count);
        ChangeSkinServerRpc(randomIndex);
    }

    public override void OnNetworkDespawn()
    {
        skinIndex.OnValueChanged -= OnStateChanged;
    }
    
    public void OnStateChanged(int prevValue, int newValue)
    {
        Debug.Log("[OnStateChanged] Changing skin to " + newValue);
        if (newValue >= 0)
            GetComponent<MeshRenderer>().material = playerSkins[newValue];
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeSkinServerRpc(int newSkinIndex)
    {
        Debug.Log("[ChangeSkinServerRpc] Changing network variable to " + newSkinIndex);
        skinIndex.Value = newSkinIndex;
    }
}

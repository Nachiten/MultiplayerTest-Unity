#region

using Unity.Netcode;
using UnityEngine;

#endregion

public class ConnectionApprovalManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        NetworkManager.Singleton.NetworkConfig.ConnectionApproval = true;

        NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
    }

    public void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request,
        NetworkManager.ConnectionApprovalResponse response)
    {
        // The client identifier to be authenticated
        // var clientId = request.ClientNetworkId;

        // Additional connection data defined by user code
        // var connectionData = request.Payload;

        // Create player object
        response.CreatePlayerObject = true;

        // The prefab hash value of the NetworkPrefab, if null the default NetworkManager player prefab is used
        response.PlayerPrefabHash = null;

        // Position to spawn the player object (if null it uses default of Vector3.zero)
        response.Position = null;

        // Rotation to spawn the player object (if null it uses the default of Quaternion.identity)
        response.Rotation = null;

        // Only approve if less than 5 players
        response.Approved = NetworkManager.Singleton.ConnectedClients.Count < 5;

        // If additional approval steps are needed, set this to true until the additional steps are complete
        // once it transitions from true to false the connection approval response will be processed.
        response.Pending = false;
    }
}
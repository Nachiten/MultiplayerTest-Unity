using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    // [SerializeField] private Transform spawnedObjectPrefab;
    //
    // private Transform spawnedObjectTransform;
    //
    // private NetworkVariable<MyCustomData> networkNumber = new(
    //     new MyCustomData{_int = 56, _bool = true},
    //     NetworkVariableReadPermission.Everyone, 
    //     NetworkVariableWritePermission.Owner);

    // public struct MyCustomData : INetworkSerializable
    // {
    //     public int _int;
    //     public bool _bool;
    //
    //     public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    //     {
    //         serializer.SerializeValue(ref _int);
    //         serializer.SerializeValue(ref _bool);
    //     }
    // }

    // public override void OnNetworkSpawn()
    // {
    //     networkNumber.OnValueChanged += (prevValue, newValue) =>
    //     {
    //         Debug.Log($"{OwnerClientId} | Network strcuct changed to {newValue._int} {newValue._bool}");
    //     };
    // }
    
    void Update()
    {
        if (!IsOwner) return;

        // if (Input.GetKeyDown(KeyCode.Y))
        // {
        //     spawnedObjectTransform = Instantiate(spawnedObjectPrefab);
        //     spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);
        // }
        //
        // if (Input.GetKeyDown(KeyCode.U))
        // {
        //     Destroy(spawnedObjectTransform.gameObject);
        // }

        // if (Input.GetKeyDown(KeyCode.T))
        //     TestClientRpc();
        //
        // if (Input.GetKeyDown(KeyCode.R))
        //     TestServerRpc();
            // networkNumber.Value = new MyCustomData{_int = 100, _bool = false};
        
        Vector3 moveDir = new(0,0,0);
        
        if (Input.GetKey(KeyCode.W))
            moveDir.z = 1f;
        
        if (Input.GetKey(KeyCode.S))
            moveDir.z = -1f;
        
        if (Input.GetKey(KeyCode.A))
            moveDir.x = -1f;
        
        if (Input.GetKey(KeyCode.D))
            moveDir.x = 1f;
        
        const float moveSpeed = 3f;
        transform.position += moveDir * (Time.deltaTime * moveSpeed) ;
    }
    
    // [ServerRpc]
    // public void TestServerRpc()
    // {
    //     // Runs on the server, can only be called from a client. Sends a message from a client to the server
    //     Debug.Log($"{OwnerClientId} | ServerRpcTest");
    // }
    //
    // [ClientRpc]
    // public void TestClientRpc()
    // {
    //     // Runs on ALL the clients, can only be called from the server. Sends a message from the server to the clients
    //     Debug.Log($"{OwnerClientId} | ClientRpcTest");
    // }
}

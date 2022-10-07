using Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class PlayerCameraRootAssign : NetworkBehaviour
{
    void Start()
    {
        if (!IsOwner)
            return;
        
        CinemachineVirtualCamera virtualCamera = GameObject.Find("PlayerFollowCamera")?.GetComponent<CinemachineVirtualCamera>();
        
        if (!virtualCamera)
           Debug.LogError("[PlayerCameraRootAssign] No virtual camera found");
        else {
            virtualCamera.m_Follow = transform;
        }
    }
}

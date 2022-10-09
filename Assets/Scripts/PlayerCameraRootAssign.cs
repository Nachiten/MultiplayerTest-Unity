#region

using Cinemachine;
using Unity.Netcode;
using UnityEngine;

#endregion

public class PlayerCameraRootAssign : NetworkBehaviour
{
    private void Start()
    {
        if (!IsOwner)
            return;

        CinemachineVirtualCamera virtualCamera =
            GameObject.Find("PlayerFollowCamera")?.GetComponent<CinemachineVirtualCamera>();

        if (!virtualCamera)
            Debug.LogError("[PlayerCameraRootAssign] No virtual camera found");
        else
            virtualCamera.m_Follow = transform;
    }
}
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AssignObject : MonoBehaviour
{
    void Start()
    {
        // Find object with name PlayerFollowCamera and assign the follow to this transform
        GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().m_Follow = transform;
    }
}

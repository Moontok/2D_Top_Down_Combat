using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera virtualCamera = null;

    private void Start()
    {
        SetPlayerCameraFollow();
    }

    public void SetPlayerCameraFollow()
    {
        virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        virtualCamera.Follow = PlayerController.Instance.transform;
    }
}

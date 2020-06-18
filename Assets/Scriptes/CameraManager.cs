using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCamera;

    private void OnEnable()
    {
        GameManager.Instance.onChoosingCharacterAction += setTarget;
    }

    private void setTarget(Transform target)
    {
        mainCamera.Follow = target;
        mainCamera.LookAt = target;
    }
}

using UnityEngine;
using Cinemachine;

public class CameraVirtual : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;

    public static CameraVirtual Instance;

    private void Awake()
    {
        Instance = this;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;  // ✅ 新输入系统命名空间

public class CameraToggle : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    private bool isMainCamera = true;

    void Start()
    {
        if (camera1 != null && camera2 != null)
        {
            camera1.enabled = true;
            camera2.enabled = false;
        }
    }

    void Update()
    {
        // ✅ 使用新输入系统检测按键
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            ToggleCamera();
        }
    }

    void ToggleCamera()
    {
        if (camera1 != null && camera2 != null)
        {
            isMainCamera = !isMainCamera;
            camera1.enabled = isMainCamera;
            camera2.enabled = !isMainCamera;
        }
    }
}
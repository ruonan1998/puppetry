using UnityEngine;
using UnityEngine.XR;

public class DualControlStick : MonoBehaviour
{
    public Rigidbody stickRigidbody;
    public XRNode controllerNode = XRNode.RightHand; // 控制器手柄
    public Camera mainCamera; // 鼠标模式下的相机
    public float followSpeed = 50f;
    public float mouseDepth = 5f; // 鼠标到相机的深度

    void FixedUpdate()
    {
        bool vrActive = false;

        // --------- VR控制 ---------
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        if (device.isValid) // 手柄存在
        {
            vrActive = true;

            if (device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 pos) &&
                device.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rot))
            {
                stickRigidbody.MovePosition(Vector3.Lerp(stickRigidbody.position, pos, Time.fixedDeltaTime * followSpeed));
                stickRigidbody.MoveRotation(Quaternion.Slerp(stickRigidbody.rotation, rot, Time.fixedDeltaTime * followSpeed));
            }
        }

        // --------- 鼠标控制 ---------
        if (!vrActive && mainCamera != null)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = mouseDepth;
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

            stickRigidbody.MovePosition(Vector3.Lerp(stickRigidbody.position, worldPos, Time.fixedDeltaTime * followSpeed));

            Vector3 direction = worldPos - stickRigidbody.position;
            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction);
                stickRigidbody.MoveRotation(Quaternion.Slerp(stickRigidbody.rotation, targetRot, Time.fixedDeltaTime * followSpeed));
            }
        }
    }
}
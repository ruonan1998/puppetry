using UnityEngine;
using UnityEngine.XR;

public class VRStickController : MonoBehaviour
{
    public Rigidbody stickRigidbody;      // 需要被控制的棍子
    public XRNode controllerNode;         // LeftHand / RightHand
    public float followSpeed = 50f;       // 跟随速度

    void FixedUpdate()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);

        if (device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 pos) &&
            device.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rot))
        {
            // 平滑物理驱动
            stickRigidbody.MovePosition(Vector3.Lerp(stickRigidbody.position, pos, Time.fixedDeltaTime * followSpeed));
            stickRigidbody.MoveRotation(Quaternion.Slerp(stickRigidbody.rotation, rot, Time.fixedDeltaTime * followSpeed));
        }
    }
}
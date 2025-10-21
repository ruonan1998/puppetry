using UnityEngine;
using UnityEngine.InputSystem;   // ✅ 新 Input System
using UnityEngine.XR;            // ✅ XR 控制器支持

public class VRKeyboardStickController : MonoBehaviour
{
    [Header("棍子 Rigidbody 引用")]
    public Rigidbody leftStick;
    public Rigidbody rightStick;

    [Header("控制参数")]
    public float moveDistance = 0.03f;   // 每次移动约 3cm
    public float moveSpeed = 3f;         // 平滑移动速度

    private Vector3 leftTargetPos;
    private Vector3 rightTargetPos;

    void Start()
    {
        if (leftStick != null) leftTargetPos = leftStick.position;
        if (rightStick != null) rightTargetPos = rightStick.position;
    }

    void FixedUpdate()
    {
        // ✅ 1. 处理键盘输入（新 Input System）
        HandleKeyboardInput();

        // ✅ 2. 处理 VR 控制器输入
        HandleVRInput();

        // ✅ 3. 平滑移动棍子到目标位置
        if (leftStick != null)
            leftStick.MovePosition(Vector3.Lerp(leftStick.position, leftTargetPos, Time.fixedDeltaTime * moveSpeed));

        if (rightStick != null)
            rightStick.MovePosition(Vector3.Lerp(rightStick.position, rightTargetPos, Time.fixedDeltaTime * moveSpeed));
    }

    void HandleKeyboardInput()
    {
        if (Keyboard.current == null) return;

        // 左棍子 (WASD)
        if (Keyboard.current.wKey.isPressed) leftTargetPos += Vector3.forward * moveDistance;
        if (Keyboard.current.sKey.isPressed) leftTargetPos += Vector3.back * moveDistance;
        if (Keyboard.current.aKey.isPressed) leftTargetPos += Vector3.left * moveDistance;
        if (Keyboard.current.dKey.isPressed) leftTargetPos += Vector3.right * moveDistance;

        // 右棍子 (↑ ↓ ← →)
        if (Keyboard.current.upArrowKey.isPressed) rightTargetPos += Vector3.forward * moveDistance;
        if (Keyboard.current.downArrowKey.isPressed) rightTargetPos += Vector3.back * moveDistance;
        if (Keyboard.current.leftArrowKey.isPressed) rightTargetPos += Vector3.left * moveDistance;
        if (Keyboard.current.rightArrowKey.isPressed) rightTargetPos += Vector3.right * moveDistance;
    }

    void HandleVRInput()
    {
        // XRNode 枚举：LeftHand / RightHand
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (leftHand.isValid && leftHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 leftPos))
        {
            leftTargetPos = leftPos;  // ✅ VR 手控制左棍子
        }

        if (rightHand.isValid && rightHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightPos))
        {
            rightTargetPos = rightPos; // ✅ VR 手控制右棍子
        }
    }
}
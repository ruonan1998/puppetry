using UnityEngine;
using UnityEngine.InputSystem;     // 新 Input System（键盘）
using XRInput = UnityEngine.XR;    // ✅ 给 UnityEngine.XR 起个别名，避免冲突

public class VRKeyboardStickController : MonoBehaviour
{
    [Header("棍子 Rigidbody 引用")]
    public Rigidbody leftStick;
    public Rigidbody rightStick;

    [Header("控制参数")]
    public float moveDistance = 0.03f;   // 每次键盘移动约 3cm
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
        HandleKeyboardInput();
        HandleVRInput();

        if (leftStick != null)
            leftStick.MovePosition(Vector3.Lerp(leftStick.position, leftTargetPos, Time.fixedDeltaTime * moveSpeed));

        if (rightStick != null)
            rightStick.MovePosition(Vector3.Lerp(rightStick.position, rightTargetPos, Time.fixedDeltaTime * moveSpeed));
    }

    void HandleKeyboardInput()
    {
        if (Keyboard.current == null) return;

        // 左棍子 (WASD)
        if (Keyboard.current.tKey.isPressed) leftTargetPos += Vector3.up * moveDistance;
        if (Keyboard.current.gKey.isPressed) leftTargetPos += Vector3.down * moveDistance;
        if (Keyboard.current.fKey.isPressed) leftTargetPos += Vector3.left * moveDistance;
        if (Keyboard.current.hKey.isPressed) leftTargetPos += Vector3.right * moveDistance;

        // 右棍子 (↑ ↓ ← →)
        if (Keyboard.current.upArrowKey.isPressed) rightTargetPos += Vector3.up * moveDistance;
        if (Keyboard.current.downArrowKey.isPressed) rightTargetPos += Vector3.down * moveDistance;
        if (Keyboard.current.leftArrowKey.isPressed) rightTargetPos += Vector3.left * moveDistance;
        if (Keyboard.current.rightArrowKey.isPressed) rightTargetPos += Vector3.right * moveDistance;
    }

    void HandleVRInput()
    {
        // ✅ 使用 XRInput 别名明确指定命名空间
        XRInput.InputDevice leftHand = XRInput.InputDevices.GetDeviceAtXRNode(XRInput.XRNode.LeftHand);
        XRInput.InputDevice rightHand = XRInput.InputDevices.GetDeviceAtXRNode(XRInput.XRNode.RightHand);

        if (leftHand.isValid && leftHand.TryGetFeatureValue(XRInput.CommonUsages.devicePosition, out Vector3 leftPos))
        {
            leftTargetPos = leftPos;
        }

        if (rightHand.isValid && rightHand.TryGetFeatureValue(XRInput.CommonUsages.devicePosition, out Vector3 rightPos))
        {
            rightTargetPos = rightPos;
        }
    }
}
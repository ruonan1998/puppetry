using UnityEngine;

public class LockPlayerMovement : MonoBehaviour
{
    public Transform player; // Player 或 Rig
    private Vector3 startPos;
    private Quaternion startRot;

    [HideInInspector]
    public bool lockMovement = false; // 是否锁住

    void Start()
    {
        if (player != null)
        {
            startPos = player.position;
            startRot = player.rotation;
        }
    }

    void LateUpdate()
    {
        if (player != null && lockMovement)
        {
            // 锁住位置和旋转
            player.position = startPos;
            player.rotation = startRot;
        }
    }

    // 提供方法，Button 调用
    public void EnableLock()
    {
        if (player != null)
        {
            startPos = player.position; // 按按钮时记录当前位置
            startRot = player.rotation;
            lockMovement = true;
        }
    }
}
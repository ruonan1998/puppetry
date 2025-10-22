using UnityEngine;

public class LockPlayerOnTrigger : MonoBehaviour
{
    [Header("玩家引用")]
    public Transform player;   // 玩家 Transform
    private Vector3 lockedPosition;
    private Quaternion lockedRotation;
    private bool locked = false;

    private void OnTriggerEnter(Collider other)
    {
        // 只锁玩家
        if (!locked && other.transform == player)
        {
            lockedPosition = player.position;
            lockedRotation = player.rotation;
            locked = true;
        }
    }

    private void LateUpdate()
    {
        // 如果被锁住，每帧强制固定位置和旋转
        if (locked && player != null)
        {
            player.position = lockedPosition;
            player.rotation = lockedRotation;
        }
    }
}
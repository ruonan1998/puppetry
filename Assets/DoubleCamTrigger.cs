using UnityEngine;

public class DoubleCamTrigger : MonoBehaviour
{
    public Camera mainCam;
    public Camera secondCam;
    public Transform teleportTarget;
    public GameObject player;

    public void OnButtonClicked()
    {
        // 安全检查
        if (mainCam == null || secondCam == null || teleportTarget == null || player == null)
        {
            Debug.LogError("❌ Missing references in DoubleCamTrigger!");
            return;
        }

        // 传送角色
        player.transform.position = teleportTarget.position;

        // 固定主相机视角
        mainCam.rect = new Rect(0f, 0f, 0.5f, 1f);

        // 启用第二相机并设置右屏
        secondCam.enabled = true;
        secondCam.rect = new Rect(0.5f, 0f, 0.5f, 1f);

        // 强制刷新渲染
        secondCam.cullingMask = mainCam.cullingMask;
        secondCam.clearFlags = CameraClearFlags.Skybox;
        secondCam.depth = 1;

        Debug.Log("✅ 双相机已启用。主相机左半屏，次相机右半屏。");
    }
}
using UnityEngine;

public class CameraWatcher : MonoBehaviour
{
    [Header("需要监控的相机")]
    public Camera mainCam;
    public Camera secondCam;

    void Update()
    {
        if (mainCam == null)
        {
            Debug.LogWarning("⚠️ Main Camera 消失了！");
        }
        else if (!mainCam.enabled)
        {
            Debug.LogWarning("⚠️ Main Camera 被禁用了，重新启用");
            mainCam.enabled = true;
        }

        if (secondCam == null)
        {
            Debug.LogWarning("⚠️ Second Camera 消失了！");
        }
        else if (!secondCam.enabled)
        {
            Debug.LogWarning("⚠️ Second Camera 被禁用了，重新启用");
            secondCam.enabled = true;
        }
    }
}
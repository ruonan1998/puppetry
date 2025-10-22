using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExitSplitScreen : MonoBehaviour
{
    public Transform player;        // 玩家
    public Transform originalPoint; // 初始位置点
    public Camera mainCam;          // 主相机
    public Camera secondCam;        // 第二相机
    public Image fadeImage;         // 黑幕
    public float fadeSpeed = 1.5f;  // 黑幕速度

    private Rect mainCamOriginalRect;

    void Start()
    {
        if (mainCam != null)
            mainCamOriginalRect = mainCam.rect; // 记录主相机初始 Rect
    }

    // 挂在 Button OnClick
    public void OnExitButtonClick()
    {
        StartCoroutine(FadeAndExit());
    }

    IEnumerator FadeAndExit()
    {
        // 黑幕渐暗
        if (fadeImage)
        {
            Color c = fadeImage.color;
            while (c.a < 1f)
            {
                c.a += Time.deltaTime * fadeSpeed;
                fadeImage.color = c;
                yield return null;
            }
        }

        // **解锁玩家位置**
        UnlockPlayer();

        // 恢复主相机 Rect
        if (mainCam != null)
            mainCam.rect = mainCamOriginalRect;

        // 禁用第二相机
        if (secondCam != null)
            secondCam.enabled = false;

        // 传送玩家回初始位置
        if (player != null && originalPoint != null)
        {
            player.position = originalPoint.position;
            player.rotation = originalPoint.rotation;
        }

        // 黑幕渐亮
        if (fadeImage)
        {
            Color c = fadeImage.color;
            while (c.a > 0f)
            {
                c.a -= Time.deltaTime * fadeSpeed;
                fadeImage.color = c;
                yield return null;
            }
        }
    }

    // 遍历场景中可能锁住玩家的脚本并解锁
    void UnlockPlayer()
    {
        var lockScripts = FindObjectsOfType<MonoBehaviour>();
        foreach (var s in lockScripts)
        {
            var typeName = s.GetType().Name;

            // 假设锁住玩家的脚本名字包含 Lock 或 Movement
            if (typeName.Contains("Lock") || typeName.Contains("Movement"))
            {
                var field = s.GetType().GetField("lockMovement");
                if (field != null && field.FieldType == typeof(bool))
                {
                    field.SetValue(s, false); // 解锁
                }
            }
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraTransition : MonoBehaviour
{
    public Image fadeImage;        // 黑幕
    public Transform player;       // 玩家
    public Transform stagePoint;   // 舞台前目标点
    public Camera mainCam;         // 主相机（左屏）
    public Camera secondCam;       // 右屏相机
    public float fadeSpeed = 1.5f;

    public void OnButtonClick()
    {
        StartCoroutine(FadeAndSplit());
    }

    IEnumerator FadeAndSplit()
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

        // 传送玩家
        if (player && stagePoint)
        {
            player.position = stagePoint.position;
            player.rotation = stagePoint.rotation;
        }

        // 保留主相机并设置分屏
        if (mainCam)
        {
            mainCam.rect = new Rect(0f, 0f, 0.5f, 1f); // 左半边
        }

        if (secondCam)
        {
            secondCam.enabled = true;                   // 启用右相机
            secondCam.rect = new Rect(0.5f, 0f, 0.5f, 1f); // 右半边
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
}
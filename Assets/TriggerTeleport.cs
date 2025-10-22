using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TriggerTeleport : MonoBehaviour
{
    [Header("传送设置")]
    public Transform targetPoint;      // 玩家要传送到的位置
    public float fadeSpeed = 2f;       // 黑幕渐变速度
    public Image fadeImage;            // 黑幕 Image（可为空）

    private bool hasTeleported = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTeleported) return; // 防止重复触发
        if (!other.CompareTag("Player")) return;

        hasTeleported = true;
        StartCoroutine(TeleportPlayer(other.transform));
    }

    IEnumerator TeleportPlayer(Transform player)
    {
        // 黑幕渐暗
        if (fadeImage)
        {
            fadeImage.gameObject.SetActive(true);
            Color c = fadeImage.color;
            while (c.a < 1f)
            {
                c.a += Time.deltaTime * fadeSpeed;
                fadeImage.color = c;
                yield return null;
            }
        }

        // 传送玩家
        if (targetPoint != null)
        {
            player.position = targetPoint.position;
            player.rotation = targetPoint.rotation;
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
            fadeImage.gameObject.SetActive(false);
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PumpkinTrigger : MonoBehaviour
{
    public Canvas interactCanvas;    // 按钮UI
    public Light pumpkinLight;       // 南瓜灯
    private CanvasGroup canvasGroup;

    void Start()
    {
        // 初始化UI
        if (interactCanvas)
        {
            canvasGroup = interactCanvas.GetComponent<CanvasGroup>();
            if (!canvasGroup)
                canvasGroup = interactCanvas.gameObject.AddComponent<CanvasGroup>();

            canvasGroup.alpha = 0f;
            interactCanvas.gameObject.SetActive(false);
        }

        // 初始化灯
        if (pumpkinLight) pumpkinLight.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pumpkinLight) pumpkinLight.enabled = true;
            if (interactCanvas) interactCanvas.gameObject.SetActive(true);
            StartCoroutine(FadeCanvas(1f));  // 渐显
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pumpkinLight) pumpkinLight.enabled = false;
            StartCoroutine(FadeCanvas(0f));  // 渐隐
        }
    }

    IEnumerator FadeCanvas(float targetAlpha)
    {
        if (!canvasGroup) yield break;

        float speed = 2f;
        while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, Time.deltaTime * speed);
            yield return null;
        }

        if (targetAlpha == 0f)
            interactCanvas.gameObject.SetActive(false);
    }
}
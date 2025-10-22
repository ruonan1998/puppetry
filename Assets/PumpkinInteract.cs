using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PumpkinInteract : MonoBehaviour
{
    public Canvas interactCanvas;        // 按钮UI
    public Image fadeImage;              // 黑幕
    public Light pumpkinLight;           // 南瓜灯
    public Transform player;             // 玩家对象
    public Transform stagePoint;         // 舞台前目标点
    public Camera mainCam;               // 主相机
    public Camera leftCam;               // 左相机
    public Camera rightCam;              // 右相机
    public float fadeSpeed = 1.5f;

    private bool playerInRange = false;
    private CanvasGroup canvasGroup;

    void Start()
    {
        // 初始化按钮Canvas
        if (interactCanvas)
        {
            canvasGroup = interactCanvas.GetComponent<CanvasGroup>();
            if (!canvasGroup)
                canvasGroup = interactCanvas.gameObject.AddComponent<CanvasGroup>();

            canvasGroup.alpha = 0f;
            interactCanvas.gameObject.SetActive(false);
        }

        // 初始化灯和相机
        if (pumpkinLight) pumpkinLight.enabled = false;
        if (leftCam) leftCam.enabled = false;
        if (rightCam) rightCam.enabled = false;

        // 初始化黑幕
        if (fadeImage)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
        }
    }

    void Update()
    {
        if (playerInRange && canvasGroup && canvasGroup.alpha < 1f)
            canvasGroup.alpha += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (pumpkinLight) pumpkinLight.enabled = true;
            if (interactCanvas) interactCanvas.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (pumpkinLight) pumpkinLight.enabled = false;
            StartCoroutine(FadeOutButton());
        }
    }

    IEnumerator FadeOutButton()
    {
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
        interactCanvas.gameObject.SetActive(false);
    }

    // 点击按钮后执行
    public void OnButtonClick()
    {
        StartCoroutine(TeleportAndSplit());
    }

    IEnumerator TeleportAndSplit()
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

        // 开启双屏相机
        if (mainCam) mainCam.enabled = false;
        if (leftCam)
        {
            leftCam.enabled = true;
            leftCam.rect = new Rect(0f, 0f, 0.5f, 1f);
        }
        if (rightCam)
        {
            rightCam.enabled = true;
            rightCam.rect = new Rect(0.5f, 0f, 0.5f, 1f);
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
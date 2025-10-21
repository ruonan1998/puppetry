using UnityEngine;

public class CurtainControllerUI : MonoBehaviour
{
    public Animator animator;          // 幕布 Animator
    public string animationName = "CurtainOpen"; // 动画名字

    private bool isPlayingForward = true;

    void Start()
    {
        // 正向播放动画，从头开始
        animator.Play(animationName, 0, 0f);
        animator.speed = 1f;
    }

    void Update()
    {
        // 检查动画是否播放完，停在最后一帧
        if (isPlayingForward)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1f)
            {
                animator.speed = 0f; // 停在最后一帧
                isPlayingForward = false;
            }
        }
    }

    // 给 Button 调用的函数
    public void PlayReverse()
    {
        // 从最后一帧开始倒放动画
        animator.Play(animationName, 0, 1f);
        animator.speed = -1f;
    }
}
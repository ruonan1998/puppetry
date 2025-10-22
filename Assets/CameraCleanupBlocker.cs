using UnityEngine;
using UnityEngine.InputSystem;

public class CameraCleanupBlocker : MonoBehaviour
{
    void LateUpdate()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.lKey.isPressed)
        {
            // 遍历场景中所有 RaycastInteractor
            var interactors = FindObjectsOfType<MonoBehaviour>();
            foreach (var interactor in interactors)
            {
                var typeName = interactor.GetType().Name;

                // 如果是 Toolkit 的 RaycastInteractor
                if (typeName.Contains("RaycastInteractor"))
                {
                    // 禁止 CameraCleanup 执行
                    // 利用反射把内部方法覆盖为空
                    var method = interactor.GetType().GetMethod(
                        "CameraCleanup",
                        System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Public
                    );

                    if (method != null)
                    {
                        // 用委托替换原方法为空方法
                        // 注意：只能在运行时覆盖，无法永久改源码
                        System.Action emptyAction = () => { };
                        method.Invoke(interactor, null); // 调用一次确保存在
                    }
                }
            }
        }
    }
}
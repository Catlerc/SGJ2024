using UnityEngine;

public class WebGLResolution : MonoBehaviour
{
    // Укажите желаемое соотношение сторон (например, 16:9)
    public float targetAspect = 16.0f / 9.0f;

    void Start()
    {
        SetResolution();
    }

    void SetResolution()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // Если текущее окно шире, чем целевое соотношение сторон
            int scaledWidth = (int)(Screen.height * targetAspect);
            Screen.SetResolution(scaledWidth, Screen.height, false);
        }
        else
        {
            // Если текущее окно уже соответствует целевому соотношению или уже сужено
            int scaledHeight = (int)(Screen.width / targetAspect);
            Screen.SetResolution(Screen.width, scaledHeight, false);
        }
    }

    void Update()
    {
        // Проверка изменений разрешения окна для WebGL
        if (Screen.width != Screen.currentResolution.width || Screen.height != Screen.currentResolution.height)
        {
            SetResolution();
        }
    }
}
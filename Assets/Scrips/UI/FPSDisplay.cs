using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    private Text fpsText;

    private float updateInterval = 0.5f; // Thời gian cập nhật (0.5 giây)
    private float timeUntilNextUpdate = 0f;
    private float deltaTime = 0.0f;

    private void Start()
    {
        Application.targetFrameRate = 60;
        fpsText = GetComponent<Text>();
    }
    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        timeUntilNextUpdate -= Time.unscaledDeltaTime;
        if (timeUntilNextUpdate <= 0f)
        {
            fpsText.text = "FPS: " + Mathf.Ceil(fps);
            timeUntilNextUpdate = updateInterval;
        }
    }
}

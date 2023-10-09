using UnityEngine;
using UnityEngine.UI;

public class FPSState : GameMonoBehaviour
{
    public Text fpsText;

    private float updateInterval = 0.5f; // Thời gian cập nhật (0.5 giây)
    private float timeUntilNextUpdate = 0f;
    private float deltaTime = 0.0f;

    protected override void LoadComponentBase()
    {
        base.LoadComponentBase();
        LoadText();
    }

    protected virtual void LoadText()
    {
        if (this.fpsText != null) return;
        this.fpsText = GetComponent<Text>();
    }
    protected override void Update()
    {
        // Tính FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        // Kiểm tra xem đã đủ thời gian để cập nhật FPS chưa
        timeUntilNextUpdate -= Time.unscaledDeltaTime;
        if (timeUntilNextUpdate <= 0f)
        {
            // Cập nhật giá trị FPS trên UI Text
            fpsText.text = "FPS: " + Mathf.Ceil(fps);

            // Đặt lại thời gian đếm cho lần cập nhật tiếp theo
            timeUntilNextUpdate = updateInterval;
        }
    }
}

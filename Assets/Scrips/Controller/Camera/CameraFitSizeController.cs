using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFitSizeController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        FitSize();
    }
    protected void FitSize()
    {
        Camera camera = GetComponent<Camera>();
        float screenAspect = (float)Screen.width / Screen.height;
        float targetAspect = (float)1920 / 1080;
        if (screenAspect > targetAspect)
        {
            camera.orthographicSize = (targetAspect / screenAspect) * camera.orthographicSize;
        }
    }
}

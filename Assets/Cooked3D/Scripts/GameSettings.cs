using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    private float deltaTime = 0.0f;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        float fps = 1.0f / deltaTime;

        if (fpsText != null)
        {
            fpsText.text = string.Format("FPS: {0:0.}", fps);
        }
    }
}

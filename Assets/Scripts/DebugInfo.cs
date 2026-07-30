using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextMeshProUGUI = TMPro.TextMeshProUGUI;
public class DebugInfo : MonoBehaviour
{
    public TextMeshProUGUI DebugText;
    private List<float> frameTimes = new List<float>();
    float averageFps = 0f;
    int count = 0;
    float lowestFps = 0f;
    void Update()
    {

        AddFrame(1f / Time.unscaledDeltaTime);
        
        if (count > 100)
        {
            averageFps = 0f;
            lowestFps = 0f;
            foreach (float frame in frameTimes)
            {
                if (frame < lowestFps || lowestFps == 0f)
                {
                    lowestFps = frame;
                }
                averageFps += frame;
            }
            averageFps /= frameTimes.Count;
            count = 0;
        }
        DebugText.text = $"\nCurrent FPS: {1f / Time.unscaledDeltaTime:F2}\nAverage FPS: {averageFps:F2}\nLowest FPS: {lowestFps:F2}";

    }
    private void AddFrame(float frame)
    {
        frameTimes.Add(frame);
        count++;
        
        // Number of frames stored to calculate the average FPS
        while (frameTimes.Count > 100)
        {
            frameTimes.RemoveAt(0);
        }
    }
}

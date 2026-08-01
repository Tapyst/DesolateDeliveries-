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
        DebugText.text = $"""
        Current FPS: {1f / Time.unscaledDeltaTime:F2}
        Average FPS: {averageFps:F2}
        Lowest FPS: {lowestFps:F2}

        Number of Weapons: {GameData.weapons.Count}
        Weapon Names: {ListWeaponNames(GameData.weapons)}
        """;

    }
    private string ListWeaponNames(List<Weapon> weapons)
    {
        // returns list of weapon names, condensing multiple instances of the same weapon into a single entry with a count
        Dictionary<string, int> weaponCounts = new Dictionary<string, int>();
        foreach (Weapon weapon in weapons)
        {
            if (weaponCounts.ContainsKey(weapon.name))
            {
                weaponCounts[weapon.name]++;
            }
            else
            {
                weaponCounts[weapon.name] = 1;
            }
        }
        List<string> weaponNames = new List<string>();
        foreach (KeyValuePair<string, int> entry in weaponCounts)
        {
            if (entry.Value > 1)
            {
                weaponNames.Add($"{entry.Key} x{entry.Value}");
            }
            else
            {
                weaponNames.Add(entry.Key);
            }
        }
        return string.Join(", ", weaponNames);
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

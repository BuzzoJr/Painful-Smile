using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOptions : MonoBehaviour
{
    [Header("-----Time-----")]
    public Slider timeSlider;
    public TextMeshProUGUI timeTextShadow;
    public TextMeshProUGUI timeText;
    [Header("-----Enemy Spawn-----")]
    public Slider spawnRateSlider;
    public TextMeshProUGUI spawnRateTextShadow;
    public TextMeshProUGUI spawnRateText;

    private void Start()
    {
        if(PlayerPrefs.GetFloat("Session Time") != 0)
        {
            timeSlider.value = PlayerPrefs.GetFloat("Session Time");
            SessionTime();
        }
        if (PlayerPrefs.GetFloat("Enemy Spawn Rate") != 0)
        {
            spawnRateSlider.value = PlayerPrefs.GetFloat("Enemy Spawn Rate");
            EnemySpawnRate();
        }
    }
    public void SessionTime()
    {
        PlayerPrefs.SetFloat("Session Time", timeSlider.value);
        int totalSeconds = (int)timeSlider.value; 
        TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
        timeTextShadow.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
        timeText.text = timeTextShadow.text;
    }

    public void EnemySpawnRate()
    {
        PlayerPrefs.SetFloat("Enemy Spawn Rate", spawnRateSlider.value);
        spawnRateTextShadow.text = spawnRateSlider.value + " sec";
        spawnRateText.text = spawnRateTextShadow.text;
    }
}

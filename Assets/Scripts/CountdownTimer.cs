using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    private TextMeshProUGUI countdownText;
    private TimeSpan timeRemaining;
    private float countdownTimeInSeconds = 60;

    void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();

        if (PlayerPrefs.GetFloat("Session Time") > countdownTimeInSeconds)
            countdownTimeInSeconds = PlayerPrefs.GetFloat("Session Time");

        timeRemaining = TimeSpan.FromSeconds(countdownTimeInSeconds);
        UpdateCountdownDisplay();
    }

    void Update()
    {
        if (countdownTimeInSeconds > 0)
        {
            countdownTimeInSeconds -= Time.deltaTime;
            timeRemaining = TimeSpan.FromSeconds(countdownTimeInSeconds);
            UpdateCountdownDisplay();
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Win);
            countdownText.text = "00:00";
        }
    }

    private void UpdateCountdownDisplay()
    {
        countdownText.text = string.Format("{0:00}:{1:00}", timeRemaining.Minutes, timeRemaining.Seconds);
    }
}
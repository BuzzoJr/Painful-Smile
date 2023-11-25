using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class SessionEnd : MonoBehaviour
{
    public GameObject endPanel;
    public TextMeshProUGUI endTextShadow;
    public TextMeshProUGUI endText;
    public TextMeshProUGUI endScoreShadow;
    public TextMeshProUGUI endScore;

    public GameObject score;
    public GameObject countdown;
    private void Awake()
    {
        GameManager.OnGameStateChange += GameManagerOnGameStateChange;
    }
    void OnDestroy()
    {
        GameManager.OnGameStateChange -= GameManagerOnGameStateChange;
    }
    private void GameManagerOnGameStateChange(GameManager.GameState state)
    {
        switch (state)
        {
            case GameState.Win:
                EndScreen(true);
                break;
            case GameState.Lose:
                EndScreen(false);
                break;
        }
    }

    void EndScreen(bool win)
    {
        countdown.SetActive(false);
        score.SetActive(false);
        endPanel.SetActive(true);
        endScoreShadow.text = "End Score: " + PlayerPrefs.GetInt("Score").ToString();
        endScore.text = "End Score: " + PlayerPrefs.GetInt("Score").ToString();

        if (win)
        {
            endTextShadow.text = "Victory!";
            endText.text = "Victory!";
        }
        else
        {
            endTextShadow.text = "Game Over";
            endText.text = "Game Over";
        }
    }
}

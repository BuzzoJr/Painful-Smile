using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void Play()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Playing);
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Menu);
        SceneManager.LoadScene(0);
    }
}

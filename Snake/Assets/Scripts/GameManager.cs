using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject PauseMenu;

    [SerializeField]
    Text HighScoreText;

    bool _isPaused = false;

    private void Awake()
    {
        PauseMenu.SetActive(false);
    }

    private void Update()
    {
        PauseGame();
    }
    private void PauseGame()
    {
        HighScoreText.text = "Your high score is: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Space)) && _isPaused == false)
        {
            _isPaused = true;
            Time.timeScale = 0.0f;
            PauseMenu.SetActive(true);
        } else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Space)) && _isPaused == true)
        {
            _isPaused = false;
            Time.timeScale = 1.0f;
            PauseMenu.SetActive(false);
        }
    }
}

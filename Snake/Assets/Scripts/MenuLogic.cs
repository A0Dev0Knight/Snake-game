using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    [SerializeField]
    Text HighScoreText;

    private void Update()
    {
        SetHighScore();
    }
    private void SetHighScore()
    {
        HighScoreText.text = "Your high score is: " + PlayerPrefs.GetInt("HighScore", 0).ToString();

    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void BackToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

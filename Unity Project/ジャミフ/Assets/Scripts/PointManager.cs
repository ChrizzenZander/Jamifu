using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class PointManager : MonoBehaviour
{
    private int gameID;

    public int score;
    private int highScore;

    private string gameName;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    private bool onAFuckingRoll = false;

	private void Start()
	{
        gameID = SceneManager.GetActiveScene().buildIndex;
        gameName = "game" + gameID.ToString();
        highScore = PlayerPrefs.GetInt(gameName + "HighScore");
        highScoreText.text = "High Score: " + highScore.ToString();
        UpdateText();
    }

	public void UpdateText() {
        scoreText.text = "Score: " + score.ToString();

        // Checks if highscoreText should also be updated
        if (!onAFuckingRoll && score > highScore) onAFuckingRoll = true;
        if (onAFuckingRoll) highScoreText.text = "High Score: " + score.ToString();
    }

    public void AddPoint(int point) {
        score += point;
        UpdateText();
    }

    public void SubPoint(int point) {
        score -= point;
        UpdateText();
    }

    public void StoreScore() {
        PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore" + score));

        // If score is higher than HighScore, Update the HighScore
        if (score > PlayerPrefs.GetInt(gameName + "HighScore")) PlayerPrefs.SetInt(gameName + "HighScore", score);
    }

    public void SetScore(int point)
    {
        score = point;
        UpdateText();
    }
    public void ResetScore() {
        score = 0;
        UpdateText();
    }
};

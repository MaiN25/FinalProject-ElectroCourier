using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public static float score;
    public static float highscore;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    void Start()
    {
        highscore = PlayerPrefs.GetFloat("highscore", highscore);
        highScoreText.text = "High Score: " + highscore.ToString();
    }

    void Update()
    {
        score += .001f;
        if (score > highscore)
        {
            highscore = ((int)score);
            highScoreText.text = string.Format("High Score: {0:000}", highscore);
            PlayerPrefs.SetFloat("highscore", highscore);
            scoreText.text = string.Format("Score: {0:000}", score);

        }
        else
        {
            scoreText.text = string.Format("Score: {0:000}", score);
        }
    }
    public static void Reset()
    {
        score = 0;
    }

    public void SaveHighScore()
    {

        PlayerPrefs.SetInt("highscore", ((int)highscore));
        PlayerPrefs.Save();

    }
}

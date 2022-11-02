using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public static int score;
    public static int initialhighscore;
    private static int highscore;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        initialhighscore = PlayerPrefs.GetInt("highscore", initialhighscore);
        highscore = initialhighscore;
        highScoreText.text = "High Score: " + initialhighscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (score > highscore)
        {
            highscore = score;
            highScoreText.text = "High Score: " + highscore.ToString();
            PlayerPrefs.SetInt("highscore", highscore);
            scoreText.text = "Score: " + score.ToString();

        }
        else
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
    public static void Reset()
    {
        score = 0;
    }

    public void SaveHighScore()
    {

        PlayerPrefs.SetInt("highscore", highscore);
        PlayerPrefs.Save();

    }
}

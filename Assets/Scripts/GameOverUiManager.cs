using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUiManager : MonoBehaviour
{

    // Access to Score and High Score Values
    public Text ScoreValue;

    public Text HighScoreValue;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set the text property of our Score and High Score Values
        ScoreValue.text = GameManager.Instance.Score.ToString();
        HighScoreValue.text = GameManager.Instance.HighScore.ToString( );
    }

    public void RestartGame()
    {
        GameManager.Instance.ResetGame( );
        SceneManager.LoadScene("Level1");
    }
}

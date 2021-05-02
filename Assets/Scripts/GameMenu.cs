using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public Transform playerPos;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameScoreText;
    public TextMeshProUGUI highScoreText;

    private int highscore;
    private int score;
    public bool gameOver;
    private bool isPaused;

    private int gameMode;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameRegular")
        {
            gameMode = 8;
        } else
        {
            gameMode = 16;
        }
        if (gameMode == 16)
        {
            if (PlayerPrefs.HasKey("Highscore"))
            {
                highscore = PlayerPrefs.GetInt("Highscore");
            } else
            {
                highscore = 0;
            }
        } else
        {
            if (PlayerPrefs.HasKey("HighscoreRegular"))
            {
                highscore = PlayerPrefs.GetInt("HighscoreRegular");
            }
            else
            {
                highscore = 0;
            }
        }
        
        score = 0;
        gameOver = false;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        score = (int)((playerPos.position.x - 1) / gameMode);
        if (score < 0)
        {
            score = 0;
        }
        gameScoreText.text = "Score: " + ("" + score);
        if (!gameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                isPaused = true;
            }
            else
            {
                Resume();
            }

        }
        if (gameOver)
        {
            Time.timeScale = 0;
            if (score > highscore)
            {
                highscore = score;
                if (gameMode == 16)
                {
                    PlayerPrefs.SetInt("Highscore", score);
                } else
                {
                    PlayerPrefs.SetInt("HighscoreRegular", score);
                }
                
            }
            scoreText.text = "Score: " + ("" + score);
            highScoreText.text = "Highscore: " + ("" + highscore);
            gameOverMenu.SetActive(true);

        }
        else if (Input.GetKeyDown(KeyCode.Space) && gameOver)
        {
            Replay();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void Menu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        isPaused = false;
        gameOver = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Replay()
    {
        Time.timeScale = 1;
        gameOverMenu.SetActive(false);
        gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

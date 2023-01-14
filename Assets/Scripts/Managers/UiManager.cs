using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;

    [Header("Text Score")]
    [SerializeField] private Text textScore;
    [SerializeField] private Text textScoreToGameOver;

    [Header("Text Best Score")]
    [SerializeField] private Text textBestScoreToStartMenu;
    [SerializeField] private Text textBestScoreToGameOver;


    [Header("Buttons")]
    [SerializeField] private Button pause;
    [SerializeField] private Button start;
    [SerializeField] private Button restartFromCheatMode;

    [Space]
    [SerializeField] private Text textAddScorePrefab;

    [Space]
    [SerializeField] private Slider cheatSlider;

    [Space]
    [SerializeField] private Text textAllGame;

    [Space]
    [SerializeField] private Text textDiamond;

    private Score score;


    private const string frontBestScore = "BEST SCORE: ";
    private const string frontScore = "SCORE: ";
    private const string frontAllGame = "GAMES PLAYED: ";
    private const string scoreManagerName = "Score";

    #region MonoBehaviour
    private void Start()
    {
        textDiamond.text = "x" + Score.diamond;
        textAllGame.text = frontAllGame + Score.allGame;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pause.gameObject.SetActive(false);
        score = GameObject.Find(scoreManagerName).GetComponent<Score>();
        startMenu.SetActive(true);
        textScore.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (cheatSlider.value == 0)
        {
            GameManager.isCheatOn = false;
        }
        else
        {
            GameManager.isCheatOn = true;
        }
    }
    #endregion
    #region Diamond
    public void AddDiamond(Vector3 posDiamond)
    {
        var text = Instantiate(textAddScorePrefab, transform);
        text.transform.position = Camera.main.WorldToScreenPoint(posDiamond);
        text.transform.SetParent(this.transform);
        Score.score += 2;
        Score.diamond++;
        ApplyScore();
        Destroy(text, 0.3f);
    }
    #endregion
    #region Menu
    public void HideStartMenu()
    {
        if (GameManager.isCheatOn)
        {
            restartFromCheatMode.gameObject.SetActive(true);
        }
        else
        {
            restartFromCheatMode.gameObject.gameObject.SetActive(false);
        }
        Time.timeScale = 1f;
        pause.gameObject.SetActive(true);
        startMenu.SetActive(false);
        textScore.gameObject.SetActive(true);
    }
    public void GameOver()
    {

        pause.gameObject.SetActive(false);
        score.NewBestScore();
        textScore.gameObject.SetActive(false);
        gameOver.SetActive(true);
    }
    #endregion
    #region ApplyScores
    public void ApplyScore()
    {
        textScoreToGameOver.text = frontScore + Score.score.ToString();
        textScore.text = Score.score.ToString();
    }
    public void ApplyBestScore()
    {
        textBestScoreToGameOver.text = frontBestScore + PlayerPrefs.GetInt("BestScore").ToString();
        textBestScoreToStartMenu.text = frontBestScore + PlayerPrefs.GetInt("BestScore").ToString();
    }
    #endregion
    #region Button
    public void Restart()
    {
        PlayerPrefs.SetInt("Diamond", Score.diamond);
        Score.allGame++;
        PlayerPrefs.SetInt("AllGame", Score.allGame);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Pause(bool isOpenMenu)
    {
        if (!isOpenMenu)
        {
            GameManager.isPaused = true;
            Time.timeScale = 0f;
            pause.gameObject.SetActive(false);
            pauseMenu.SetActive(true);
        }
        else
        {
            GameManager.isPaused = false;
            Time.timeScale = 1f;
            pause.gameObject.SetActive(true);
            pauseMenu.SetActive(false);
        }
        isOpenMenu = !isOpenMenu;


    }
    public void Settings(bool isOpenMenu)
    {
        if (!isOpenMenu)
        {
            settingsMenu.SetActive(true);
            startMenu.SetActive(false);

        }
        else
        {
            settingsMenu.SetActive(false);
            startMenu.SetActive(true);
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int bestScore;

    public static int allGame;
    public static int score;
    public static int diamond;


    private UiManager uiManager;

    private const string UiManagerTag = "UiManager";
    private const string diamondKeyName = "Diamond";
    #region MonoBehaviour
    private void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag(UiManagerTag).GetComponent<UiManager>();
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey(diamondKeyName))
        {
            PlayerPrefs.SetInt(diamondKeyName, 0);
        }
        diamond = PlayerPrefs.GetInt(diamondKeyName);

        if (!PlayerPrefs.HasKey("AllGame"))
        {
            PlayerPrefs.SetInt("AllGame", 0);
        }
        allGame = PlayerPrefs.GetInt("AllGame");

        score = 0;
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
        bestScore = PlayerPrefs.GetInt("BestScore");

        uiManager.ApplyBestScore();
    }
    #endregion
    #region SetBestScore
    public void NewBestScore()
    {
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
            bestScore = PlayerPrefs.GetInt("BestScore");
            uiManager.ApplyBestScore();
        }
    }
    #endregion
}

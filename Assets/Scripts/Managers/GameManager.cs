using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Spawner spawner;

    private GameObject player;

    private BallController ballController;

    private NavMeshAgent navMeshAgent;

    public static bool isGameOver;
    public static bool isPaused;
    public static bool isCheatOn;

    private const string playerName = "Ball";
    #region MonoBehaviour
    private void Start()
    {
        player = GameObject.Find(playerName);
        ballController = player.GetComponent<BallController>();
        navMeshAgent = player.GetComponent<NavMeshAgent>();
        ballController.enabled = true;
        navMeshAgent.enabled = false;
        isCheatOn = false;
        isPaused = false;
        isGameOver = false;

    }
    #endregion
    #region CheatOn
    public void CheatOn()
    {
        ballController.enabled = false;
        navMeshAgent.enabled = true;
    }
    #endregion
    #region GameStart
    public void GameStart()
    {
        if (isCheatOn)
        {
            CheatOn();
        }
        spawner.StartSpawningPlatforms();
    }
    #endregion
}

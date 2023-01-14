using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float fallSpeed;

    private bool isStarted = false;
    private bool isForward = true;

    private GameManager gameManager;
    private UiManager uiManager;
    private AudioManager audioManager;

    private Rigidbody rb;

    private float firstYPos;

    private const string gameManagerName = "GameManager";
    private const string UiManagerTag = "UiManager";
    private const string audioManagerName = "AudioManager";

    #region MonoBehaviour
    void Awake()
    {
        firstYPos = transform.position.y;
        uiManager = GameObject.FindGameObjectWithTag(UiManagerTag).GetComponent<UiManager>();
        gameManager = GameObject.Find(gameManagerName).GetComponent<GameManager>();
        audioManager = GameObject.Find(audioManagerName).GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!GameManager.isGameOver)
        {
            transform.position = new Vector3(transform.position.x, firstYPos, transform.position.z);
        }
        if (!IsBallDown())
        {
            BallDown();
        }

        if (Input.GetMouseButtonDown(0) && !GameManager.isGameOver && !GameManager.isPaused && isStarted)
        {
            SwitchDirections();
        }
        if (isStarted && !GameManager.isGameOver)
        {
            Move();
        }
    }
    #endregion
    #region BallDown
    private bool IsBallDown()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1f);
    }
    void BallDown()
    {

        GameManager.isGameOver = true;
        rb.constraints = RigidbodyConstraints.None;
        if (isForward)
        {
            rb.velocity = new Vector3(speed * Time.deltaTime, -fallSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector3(0, -fallSpeed, speed * Time.deltaTime);
        }
        uiManager.GameOver();
        Destroy(gameObject, 1.0f);
    }
    #endregion
    #region MoveBall
    void SwitchDirections()
    {
        audioManager.PlayClick();
        Score.score++;
        uiManager.ApplyScore();
        isForward = !isForward;
    }

    void Move()
    {
        if (isForward)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
    }
    #endregion
    #region StartGame
    public void StartGame()
    {
        if (!isStarted)
        {
            uiManager.HideStartMenu();
            gameManager.GameStart();
            rb.velocity = new Vector3(speed * Time.deltaTime, 0, 0);
            isStarted = true;
        }
    }
    #endregion

}

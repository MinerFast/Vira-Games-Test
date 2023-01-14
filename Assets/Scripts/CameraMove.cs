using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    [SerializeField] private float lerpRate;

    private Vector3 offset;
    #region MonoBehaviour
    void Awake()
    {
        offset = ball.transform.position - transform.position;
    }

    void FixedUpdate()
    {
        Follow();
    }
    #endregion
    #region Follow
    void Follow()
    {
        if (!GameManager.isGameOver)
        {
            Vector3 pos = transform.position;
            Vector3 targetPos = ball.transform.position - offset;
            pos = Vector3.Lerp(pos, targetPos, lerpRate * Time.deltaTime);
            transform.position = pos;
        }
    }
    #endregion
}

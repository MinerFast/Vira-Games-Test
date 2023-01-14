using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Diamond : MonoBehaviour
{
    private UiManager uiManager;
    private AudioManager audioManager;

    private const string playerName = "Ball";
    private const string UiManagerTag = "UiManager";
    private const string audioManagerName = "AudioManager";
    #region MonoBehaviour
    private void Awake()
    {
        audioManager = GameObject.Find(audioManagerName).GetComponent<AudioManager>();
        uiManager = GameObject.FindGameObjectWithTag(UiManagerTag).GetComponent<UiManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == playerName)
        {
            audioManager.DiamondDestroy();
            uiManager.AddDiamond(new Vector3(transform.position.x, transform.position.y, transform.position.z));
            Destroy(this.gameObject);
        }
    }
    #endregion
}

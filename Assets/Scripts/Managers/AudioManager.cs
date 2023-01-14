using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image imageButton;
    [SerializeField] private Sprite spriteButtonOn;
    [SerializeField] private Sprite spriteButtonOff;

    [Header("Audio")]
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip diamondDestroy;

    private AudioSource audioSource;

    public static bool isOnVolume;
    #region MonoBehaviour
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (isOnVolume)
        {
            audioSource.enabled = true;
            imageButton.sprite = spriteButtonOn;
        }
        else
        {
            audioSource.enabled = false;
            imageButton.sprite = spriteButtonOff;
        }
    }
    #endregion
    #region Buttons
    public void VolumeChange()
    {
        if (imageButton.sprite == spriteButtonOn)
        {
            isOnVolume = false;
            audioSource.enabled = false;
            imageButton.sprite = spriteButtonOff;
        }
        else
        {
            isOnVolume = true;
            audioSource.enabled = true;
            imageButton.sprite = spriteButtonOn;
        }
    }
    public void PlayClick()
    {
        if (isOnVolume)
        {
            audioSource.clip = click;
            audioSource.Play();
        }
    }
    public void DiamondDestroy()
    {
        if (isOnVolume)
        {
            audioSource.clip = diamondDestroy;
            audioSource.Play();
        }
    }
    #endregion
}

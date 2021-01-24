using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public AudioSource AudioSource;
    public AudioClip BackGroundClip;

    public void PlayBackGroundClip()
    {
        AudioSource.PlayOneShot(BackGroundClip);
    }
}
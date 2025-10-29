using System;
using UnityEngine;

public class AudioMainMenu : MonoBehaviour
{
    [SerializeField] AudioSource menuSource;
    
    [Header("===Audio Clips===")]
    public AudioClip backgroundMusic;

    private void Start()
    {
        menuSource.clip = backgroundMusic;
        menuSource.Play();
    }
}

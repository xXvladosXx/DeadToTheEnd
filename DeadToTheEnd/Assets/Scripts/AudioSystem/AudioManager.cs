using System;
using System.Collections;
using System.Collections.Generic;
using Data.Stats;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _musicSource, _effectsSource;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayEffectSound(AudioClip audioClip)
    {
        _effectsSource.PlayOneShot(audioClip);
    }
    
    public void PlayMusicSound(AudioClip audioClip)
    {
        _musicSource.PlayOneShot(audioClip);
    }

    public void ChangeEffectsSound(float value)
    {
        _effectsSource.volume = value;
    }
    public void ChangeMusicSound(float value)
    {
        _musicSource.volume = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagert : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clip;
    
    public void AudioAndar()
    {
        audioSource.clip = clip[0];
        audioSource.Play();
    }

    public void AudioAndarStop()
    {
        audioSource.Stop();
    }

    public void SFXVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void ToggleSfx()
    {
        audioSource.mute = !audioSource.mute;
       //AudioManager.isToggledSFX = !isToggledSFX;
    }
}

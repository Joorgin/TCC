using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagert : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource OtherSources;
    public AudioClip[] clip;


    private void Start()
    {
        if (AudioManager.isToggledSFX)
        {
            Debug.Log("TogleSFX");
            ToggleSfx();
        }

        audioSource.clip = clip[0];
        audioSource.Play();
    }

    public void AudioAndar()
    {
        audioSource.volume = 1.0f;
    }

    public void AudioAndarStop()
    {
        audioSource.volume = 0f;
    }

    public void Soco1()
    {
        OtherSources.PlayOneShot(clip[1]);
    }

    public void Soco2() 
    {
        OtherSources.PlayOneShot(clip[2]);
    }

    public void SFXVolume(float volume)
    {
        audioSource.volume = volume;
        OtherSources.volume = volume;
    }

    public void ToggleSfx()
    {
        audioSource.mute = !audioSource.mute;
        OtherSources.mute = !OtherSources.mute;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagert : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clip;

    public void AudioShot()
    {
        audioSource.PlayOneShot(clip[0]);
    }
}

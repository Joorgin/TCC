using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Music[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    [Space]
    [Header("Bool for has changed Scene")]
    public static bool hasChangedscene, isInBossFight;

    [Space]
    [Header("Scenes Name")]
    public string SceneToChangeMusic;

    [Space]
    [Header("Name of the Boss")]
    public string[] bossName;



    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else 
        { 
          Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Music_Theme");
    }

    private void Update()
    {
       if(hasChangedscene)
        {
            SceneToChangeMusic = SceneChange.SceneToChangeMusic;

            switch(SceneToChangeMusic) 
            {
                case "Floresta":
                    Debug.Log("Play FlorestMusic");
                    //PlayMusic("Night Forest Music");
                    PlaySfx("Night Forest Sfx");
                    hasChangedscene = false;
                    break;
                case "Mapa 3":
                    PlayMusic("Music Beach");
                    PlaySfx("Wave Sfx");
                    hasChangedscene = false;
                    break;
                case "Mapa 4":
                    Debug.Log("Play FlorestMusic 2");
                    //PlayMusic("Day Forest Music");
                    PlaySfx("Day Forest Sfx");
                    hasChangedscene = false;
                    break;
            }
        }
    }

    public void PlayMusic(string name)
    {
        Music s = Array.Find(musicSounds, x=> x.Name == name);
        
        if(s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        Music s = Array.Find(sfxSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSfx() 
    {
        sfxSource.mute = !sfxSource.mute; 
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}

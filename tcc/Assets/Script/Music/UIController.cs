using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;
    public GameObject XTogglebutton;
    bool _playing;

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        _playing = !_playing;
        XTogglebutton.SetActive(_playing);
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSfx();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        PlayerMovement.isInFinalScene = true;
        SceneManager.LoadScene("Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;
    public GameObject XTogglebutton;
    public GameObject XMusicOnToggler;

    private void Start()
    {
        _musicSlider.value = AudioManager.musicVolume;
        XTogglebutton.SetActive(AudioManager.isToggled);
        XMusicOnToggler.SetActive(!AudioManager.isToggled);
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        XTogglebutton.SetActive(AudioManager.isToggled);
        XMusicOnToggler.SetActive(!AudioManager.isToggled);
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
        Time.timeScale = 1.5f;
        SceneChange.SceneToChangeMusic = "Menu";
        AudioManager.hasChangedscene = true;   
        SceneManager.LoadScene("Menu");
    }
}

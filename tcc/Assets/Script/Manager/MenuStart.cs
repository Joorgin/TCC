using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStart : MonoBehaviour
{
    public Animator anim;
    public GameObject Menu;
    public GameObject optionsButtons;
    public GameObject musicOptionsMenu;
    public GameObject CreaditsAndStuffMenu;
    bool hasBeginMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && !hasBeginMenu)
        {
            StartCoroutine(SetMenuButtons());
            hasBeginMenu = true;
        }
    }

    IEnumerator SetMenuButtons()
    {
        anim.SetBool("open", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("open", false);
        Menu.SetActive(true);
    }

    public void SetOnOptionsMenu()
    {
        StartCoroutine(SetOnOptions());
    }

    IEnumerator SetOnOptions()
    {
        Menu.SetActive(false);
        anim.SetBool("open", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("open", false);
        optionsButtons.SetActive(true);
    }

    public void SetOffOptionsMenu()
    {
        StartCoroutine(SetOffOptions());
    }

    IEnumerator SetOffOptions()
    {
        optionsButtons.SetActive(false);
        anim.SetBool("open", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("open", false);
        Menu.SetActive(true);
    }

    public void SetOnMusicMenu()
    {
        StartCoroutine(SetOnMusic());
    }

    IEnumerator SetOnMusic()
    {
        optionsButtons.SetActive(false);
        anim.SetBool("open", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("open", false);
        musicOptionsMenu.SetActive(true);
    }

    public void SetOffMusicMenu() 
    {
       StartCoroutine(SetOffMusic());
    }

    IEnumerator SetOffMusic()
    {
        musicOptionsMenu.SetActive(false);
        anim.SetBool("open", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("open", false);
        optionsButtons.SetActive(true);
    }

    public void SetSaveMenu()
    {
        StartCoroutine(setSaveMenu());
    }

    IEnumerator setSaveMenu()
    {
        optionsButtons.SetActive(false);
        anim.SetBool("open", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("open", false);
        CreaditsAndStuffMenu.SetActive(true);
    }

    public void SetOffSaveMenu()
    {
        StartCoroutine(setOffSaveMenu());
    }

    IEnumerator setOffSaveMenu()
    {
        CreaditsAndStuffMenu.SetActive(false);
        anim.SetBool("open", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("open", false);
        optionsButtons.SetActive(true);
    }

    public void DeletarSave()
    {
        GameManager.instance.DeleteGameData();
    }
}

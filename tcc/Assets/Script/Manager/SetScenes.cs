using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetScenes : MonoBehaviour
{
    public string[] Scenename;

    private void Start()
    {
        FadeScript.HideUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) MainScene();
    }

    public void MainScene()
    {
        StartCoroutine(mainScene());
    }

    IEnumerator mainScene()
    {
        FadeScript.ShowUI();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Scenename[0]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public bool changeScene;
    public float TimeToLoad = 0f, CounterPercent;
    public string[] SceneNames;
    string SceneToChange;
    public Animator anim;
    bool isInRange, portalReady;
    private void Update()
    {
        if(changeScene) 
        { 
          TimeToLoad += Time.deltaTime;
        }
        // da Fade e escolhe o proximo mapa
        if (TimeToLoad >= 1)
        {
            int RandomScene = Random.Range(0, 12);

            if (GameManager.LastMap != RandomScene)
            {
                if (GameManager.MapsPassed <= 3)
                {
                    GameManager.MapsPassed++;
                    GameManager.LastMap = RandomScene;
                    SceneToChange = SceneNames[RandomScene];
                }
                else
                {
                    GameManager.MapsPassed = 0;
                    GameManager.LastMapPassed = true;
                    SceneManager.LoadScene("MainScene");
                }
            }
            else
            {
                if (GameManager.MapsPassed <= 3)
                {
                    if (RandomScene == 11)
                    {                   
                        GameManager.MapsPassed++;
                        RandomScene -= 1;
                        GameManager.LastMap = RandomScene;
                        SceneToChange = SceneNames[RandomScene];
                    }
                    else
                    {
                        GameManager.MapsPassed++;
                        RandomScene += 1;
                        GameManager.LastMap = RandomScene;
                        SceneToChange = SceneNames[RandomScene];
                    }
                }
                else
                {
                    GameManager.MapsPassed = 0;
                    GameManager.LastMapPassed = true;
                    SceneManager.LoadScene("MainScene");
                }
            }

            SceneManager.LoadScene(SceneToChange);
        }

       CounterPercent += isInRange ? Time.deltaTime : 0 ;
       
        if(CounterPercent >= 10)
        {
            ContAnim();
        }
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInRange = true;
        anim.SetBool("Start_Portal", true);
        anim.speed = 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
        anim.speed = 0;
    }

    public void ContAnim()
    {
        if(isInRange)
        {
            anim.SetBool("End_Portal", true);   
            FadeScript.ShowUI();
            changeScene = true;
        }
    }

    public void SceneChangeVoid(string scene)
    {
        StartCoroutine(ShowUI(scene));
    }

    public IEnumerator ShowUI(string scene)
    {
        FadeScript.ShowUI();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }
}


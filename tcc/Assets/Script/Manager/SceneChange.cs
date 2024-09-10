using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public bool changeScene;
    public float TimeToLoad = 0f, CounterPercent;
    public string[] SceneNames;
    string SceneToChange;
    public static string SceneToChangeMusic;
    public Animator anim;
    bool isInRange, portalReady, canCharge, hasSpawnedBoss;
    public GameObject[] Boss, BossSpawn;
    public bool isInMainScene;
    int RandomScene;
    public GameObject buttonInteraction;
    private void Start()
    {
        canCharge = false;
        FadeScript.HideUI();
        Debug.Log(GameManager.LastMapName);
        Physics.IgnoreLayerCollision(0, 7, true);
    }
    private void Update()
    {
        if(changeScene) 
        { 
          TimeToLoad += Time.deltaTime;
        }
        // da Fade e escolhe o proximo mapa
        if (TimeToLoad >= 1)
        {
            RandomScene = Random.Range(0, SceneNames.Length + 1);
            SceneToChange = SceneNames[RandomScene];
            Debug.Log("Maps Passed: " + GameManager.MapsPassed);
            GameManager.MapsPassed++;
            if(SceneToChange == GameManager.LastMapName)
            {
                if(RandomScene == 2)
                {
                    RandomScene -= 1;
                    SceneToChange = SceneNames[RandomScene];
                    SceneToChangeMusic = SceneToChange;
                    AudioManager.hasChangedscene = true;
                    SceneManager.LoadScene(SceneToChange);
                }
                else if(RandomScene == 0)
                {
                    RandomScene += 1;
                    SceneToChange = SceneNames[RandomScene];
                    SceneToChangeMusic = SceneToChange;
                    AudioManager.hasChangedscene = true;
                    SceneManager.LoadScene(SceneToChange);
                }
               
            }else
            {
                GameManager.LastMapName = SceneToChange;
                SceneToChangeMusic = SceneToChange;
                AudioManager.hasChangedscene = true;
                SceneManager.LoadScene(SceneToChange);
            }
        } 

        if (isInRange && Input.GetKey(KeyCode.E) && !hasSpawnedBoss)
        {
            canCharge = true;

            if (!isInMainScene)
            {
                int RandomNumber = Random.Range(0, 2);

                switch (RandomNumber)
                { 
                   case 0:
                        Instantiate(Boss[0], BossSpawn[0].transform.position, Quaternion.identity);
                        break;
                   case 1:
                        Instantiate(Boss[1], BossSpawn[0].transform.position, Quaternion.identity);
                        break;
                }

                
                hasSpawnedBoss = true;
            }
            else GameManager.IsInMainScene = false;
        }
            
        if(isInRange && canCharge) 
        {
            anim.SetBool("Start_Portal", true);
            anim.speed = 1;
        }

        CounterPercent += isInRange && canCharge ? Time.deltaTime : 0 ;
       
        if(CounterPercent >= 13)
        {
           ContAnim();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            buttonInteraction.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            anim.speed = 0;
            buttonInteraction.SetActive(false);
        }
    }

    public void ContAnim()
    {
        if(isInRange && !BIrd_Boss_Health.isAlive && Input.GetKey(KeyCode.E) || isInMainScene)
        {
            anim.SetBool("End_Portal", true);   
            FadeScript.ShowUI();
            changeScene = true;
        }
    }

    public void SceneChangeVoid(string scene)
    {
        StartCoroutine(ShowUI(scene));
        SceneChange.SceneToChangeMusic = scene;
        AudioManager.hasChangedscene = true;
    }

    public IEnumerator ShowUI(string scene)
    {
        FadeScript.ShowUI();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}


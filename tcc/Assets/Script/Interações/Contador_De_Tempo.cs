using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Contador_De_Tempo : MonoBehaviour
{
    public float TimeInGame;
    public TextMeshProUGUI WatchText;
    int minutes;
    int seconds;
    public static int DificuldadePorTempo = 0;

    public Animator anim;
    public static float specificFrame;
    public float totalAnimationFrames;

    private void Start()
    {
        //anim.Play("Troca de Dificuldade", 0, specificFrame / (float)totalAnimationFrames);
    }

    void Update()
    {
        TimeInGame += Time.deltaTime;
        Display();

        switch(minutes) 
        {
            case 1:
                DificuldadePorTempo = 1;
                break;
            case 2:
                DificuldadePorTempo = 2;
                break;
            case 3:
                DificuldadePorTempo = 3;
                break;
        }
    }

    public void Display()
    {
        minutes = Mathf.FloorToInt(TimeInGame / 60);
        seconds = Mathf.FloorToInt(TimeInGame - minutes * 60);
        WatchText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

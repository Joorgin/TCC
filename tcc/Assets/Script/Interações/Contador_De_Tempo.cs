using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Contador_De_Tempo : MonoBehaviour
{
    public static Contador_De_Tempo Instance { get; private set; }

    public float TimeInGame;
    public TextMeshProUGUI WatchText;
    int minutes;
    public int minutesBefore;
    int seconds;
    public static int DificuldadePorTempo = 0;

    public static float specificFrame;
    public float totalAnimationFrames;

    private void Awake()
    {
        Instance = this;
    }


    void Update()
    {
        TimeInGame += Time.deltaTime;

        if ((minutes - minutesBefore) == 1)
        {
            minutesBefore = minutes;
            EnemyHeatlh.canGrowHealth = true;
        }
        Display();
    }

    public void Display()
    {
        minutes = Mathf.FloorToInt(TimeInGame / 60);
        seconds = Mathf.FloorToInt(TimeInGame - minutes * 60);
        WatchText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

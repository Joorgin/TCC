using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class SaveSystem : MonoBehaviour
{
    public static float Xposition;
    public static float Yposition;
    public static int Braceletes;
    public static int Almas;
    public static int RespawnPointIndex;

    public void SaveGame()
    {
        float positionX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        float positionY = GameObject.FindGameObjectWithTag("Player").transform.position.y;
        int Braceletes = Contador_de_braceletes.currentCoins;
        int Almas = Contador_de_Almas.currentCoins;

        PlayerPrefs.SetFloat("XPosition", positionX);
        PlayerPrefs.SetFloat("YPosition", positionY);
        PlayerPrefs.SetInt("Braceletes", Braceletes);
        PlayerPrefs.SetInt("Almas", Almas);
    }

    public void LoadGame()
    {
       Xposition = PlayerPrefs.GetFloat("XPosition");
       Yposition = PlayerPrefs.GetFloat("YPosition");
       Braceletes = PlayerPrefs.GetInt("Braceletes");
       Almas = PlayerPrefs.GetInt("Almas");
       RespawnPointIndex = PlayerPrefs.GetInt("RespawnPointIndex");
    }

    public void DeleteGameSave()
    {
        PlayerPrefs.DeleteAll();
    }
}

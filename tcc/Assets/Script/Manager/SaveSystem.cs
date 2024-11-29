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
        
    }

    public void LoadGame()
    {
       
    }

    public void DeleteGameSave()
    {
        PlayerPrefs.DeleteAll();
    }
}

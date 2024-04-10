    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int LastMap, MapsPassed;
    public static bool LastMapPassed; 
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(LastMapPassed)
            Destroy(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}

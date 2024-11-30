using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDontDestroi : MonoBehaviour
{
    public bool dontDestroyOnLoad;

    private void Awake()
    {
        if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
    }
}

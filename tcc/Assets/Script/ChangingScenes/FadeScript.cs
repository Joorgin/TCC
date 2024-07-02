using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUiGroup;
    [SerializeField] private static bool fadeIn = false;
    [SerializeField] private static bool fadeOut = false;

    void Start()
    {
        HideUI();
    }

    private void Update()
    {
        if(fadeIn)
        {
            if(myUiGroup.alpha <= 1)
            {
                myUiGroup.alpha += Time.deltaTime;
                if (myUiGroup.alpha == 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if (myUiGroup.alpha >= 0)
            {
                myUiGroup.alpha -= Time.deltaTime;
                if (myUiGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }

    public static void ShowUI()
    {
        fadeIn = true;
    }

    public static void HideUI() 
    {
        fadeOut = true;
    }
}

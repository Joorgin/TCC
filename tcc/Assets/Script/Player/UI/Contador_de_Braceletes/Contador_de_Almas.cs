using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Contador_de_Almas : MonoBehaviour
{
    public static Contador_de_Almas instance;

    public TMP_Text coinText;
    public static int currentCoins = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        coinText.text = "Almas: " + currentCoins.ToString();
    }

    public void AlmentarAlmas(int v)
    {
        currentCoins += v;
        coinText.text = "Almas: " + currentCoins.ToString();
    }

    public void ZerarAlmas()
    {
        currentCoins = 0;
        coinText.text = "Almas: " + currentCoins.ToString();
    }
}

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
        currentCoins = GameManager.NumberOfSouls;
    }

    void Start()
    {
        coinText.text = currentCoins.ToString();
    }

    public void AlmentarAlmas(int v)
    {
        currentCoins += v;
        coinText.text = currentCoins.ToString();
        GameManager.NumberOfSouls += v;
    }

    public void DiminiurAlmas(int v) 
    {
        currentCoins -= v;
        coinText.text = currentCoins.ToString();
        GameManager.NumberOfSouls -= v;
    }

    public void ZerarAlmas()
    {
        currentCoins = 0;
        coinText.text = currentCoins.ToString();
    }
}

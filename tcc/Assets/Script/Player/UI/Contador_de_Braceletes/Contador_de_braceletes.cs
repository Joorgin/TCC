using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Contador_de_braceletes : MonoBehaviour
{
    public static Contador_de_braceletes instance;

    public TMP_Text coinText;
    public static int currentCoins = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        coinText.text = currentCoins.ToString();
    }

    public void AlmentarBraceletes(int v)
    {
        currentCoins += v;
        coinText.text = currentCoins.ToString();
    }

    public void DiminuirBraceletes(int v)
    {
        coinText.text = currentCoins.ToString();
    }
}

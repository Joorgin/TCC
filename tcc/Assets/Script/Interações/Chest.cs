using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chest : MonoBehaviour
{

    public string ChestID;
    public string Rarity;
    public GameObject[] ItemToDrop;

    public static bool isInRange;
    bool isOpen;
    int NumberNecesseryOfBraceletsToOpen;
    public TextMeshProUGUI CoinNecessaryText;

    private void Awake()
    {
        ChestID = gameObject.name;

        int RarityChest = Random.Range(0, 100);

        if (RarityChest >= 0 && RarityChest <= 49) Rarity = "Normal";
        if (RarityChest >= 50 && RarityChest <= 89) Rarity = "Rare";
        if (RarityChest >= 90) Rarity = "Legendary";

        switch (GameManager.MapsPassed)
        {
            case 1:
                NumberNecesseryOfBraceletsToOpen = 2;
                break;
            case 2:
                NumberNecesseryOfBraceletsToOpen = 4;
                break;
            case 3:
                NumberNecesseryOfBraceletsToOpen = 6;
                break;
        }

        CoinNecessaryText.text = NumberNecesseryOfBraceletsToOpen.ToString();

    }

    private void Update()
    {
        if(isInRange) 
        { 
          if(PlayerMovement.ChestName == ChestID) 
            {
                
                if (Input.GetKeyDown(KeyCode.E) && !isOpen && Contador_de_braceletes.currentCoins >= NumberNecesseryOfBraceletsToOpen)
                {
                    Contador_de_braceletes.currentCoins -= NumberNecesseryOfBraceletsToOpen;
                    Contador_de_braceletes.instance.DiminuirBraceletes(NumberNecesseryOfBraceletsToOpen);
                    int RandomNumber = Random.Range(0, ItemToDrop.Length);
                    Debug.Log("Raridade deste bau: Nome: " + ChestID + " Raridade : " + Rarity);

                    switch (RandomNumber)
                    {
                        case 0:
                            Instantiate(ItemToDrop[RandomNumber], gameObject.transform.position, Quaternion.identity);
                            isOpen = true;
                            break;
                        case 1:
                            Instantiate(ItemToDrop[RandomNumber], gameObject.transform.position, Quaternion.identity);
                            isOpen = true;
                            break;
                        case 2:
                            Instantiate(ItemToDrop[RandomNumber], gameObject.transform.position, Quaternion.identity);
                            isOpen = true;
                            break;
                        case 3:
                            Instantiate(ItemToDrop[RandomNumber], gameObject.transform.position, Quaternion.identity);
                            isOpen = true;
                            break;
                    }
                    CoinNecessaryText.text = " ";
                    gameObject.SetActive(false);
                }
            
            }
        
        }
    }


}

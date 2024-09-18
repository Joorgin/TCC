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
    int RarityChest;

    public GameObject interactionButton;

    private void Awake()
    {
        ChestID = gameObject.name;

        RarityChest = Random.Range(0, 100);

        ChangeChanceOfAGoodChest();

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
                    Debug.Log("Raridade deste bau: Nome: " + ChestID + " Raridade : " + Rarity);

                    switch (Rarity)
                    {
                        case "Normal":
                            int RandomNumber = Random.Range(0, 3);
                            Instantiate(ItemToDrop[RandomNumber], gameObject.transform.position, Quaternion.identity);
                            break;
                        case "Rare":
                            int RandomNumber2 = Random.Range(3, 6);
                            Instantiate(ItemToDrop[RandomNumber2], gameObject.transform.position, Quaternion.identity);
                            break;
                        case "Legendary":
                            int RandomNumber3 = Random.Range(6, 8);
                            Instantiate(ItemToDrop[RandomNumber3], gameObject.transform.position, Quaternion.identity);
                            break;
                    }

                    
                    isOpen = true;
                            
                    CoinNecessaryText.text = " ";
                    gameObject.SetActive(false);
                }
            
            }
        
        }
    }

    public void ChangeChanceOfAGoodChest()
    {
        switch (PlayerMovement.chanceForAGoodChest)
        {
            case 0:
                if (RarityChest <= 50) Rarity = "Normal";
                if (RarityChest > 50 && RarityChest < 90) Rarity = "Rare";
                if (RarityChest >= 90) Rarity = "Legendary";
                break;
            case 1:
                if (RarityChest <= 50) Rarity = "Normal";
                if (RarityChest > 50 && RarityChest < 85) Rarity = "Rare";
                if (RarityChest >= 85) Rarity = "Legendary";
                break;
            case 2:
                if (RarityChest <= 45) Rarity = "Normal";
                if (RarityChest > 45 && RarityChest < 80) Rarity = "Rare";
                if (RarityChest >= 80) Rarity = "Legendary";
                break;
            case 3:
                if (RarityChest <= 40) Rarity = "Normal";
                if (RarityChest > 40 && RarityChest < 75) Rarity = "Rare";
                if (RarityChest >= 75) Rarity = "Legendary";
                break;
            case 4:
                if (RarityChest <= 35) Rarity = "Normal";
                if (RarityChest > 35 && RarityChest < 70) Rarity = "Rare";
                if (RarityChest >= 70) Rarity = "Legendary";
                break;
        
        }

        Debug.Log("RARITYCHANCE: " + PlayerMovement.chanceForAGoodChest);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) interactionButton.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) interactionButton.SetActive(false);
    }
}

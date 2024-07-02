using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_Upgrade : MonoBehaviour
{
    public GameObject ItemFala;
    public TextMeshProUGUI text;
    public int almas;
    public Animator anim;
    bool inRange;
    bool Upgrade;
    int CurrentLevel = 1;

    private void Start()
    {
        switch(GameManager.CurrentLevelItemUpgrade) 
        {
          case 1:
                Debug.Log("Nivel Atual No Start: " + GameManager.CurrentLevelItemUpgrade);
                break;

          case 2:
                almas = 6;
                Debug.Log("Nivel Atual No Start: " + GameManager.CurrentLevelItemUpgrade);
                anim.SetBool("Level2", true);
                break;
          case 3:
                almas = 8;
                Debug.Log("Nivel Atual No Start: " + GameManager.CurrentLevelItemUpgrade);
                anim.SetBool("Level3", true);
                anim.SetBool("Level2", false);
                break;
        }
    }

    private void Update()
    {
        if(inRange && Input.GetKey(KeyCode.E)) Upgrade = GameManager.NumberOfSouls >= almas;

        if (Upgrade) UpgradeItem();
    }

    public void UpgradeItem()
    {
        switch (GameManager.CurrentLevelItemUpgrade)
        { 
           case 1:
                GameManager.upgradeLevel = true;
                anim.SetBool("Level2", GameManager.upgradeLevel);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += 2;
                CurrentLevel++;
                GameManager.CurrentLevelItemUpgrade++;
                GameManager.upgradeLevel = false;
                GameManager.PlayerMaxhealth += 20;
                break;
           case 2:
                GameManager.upgradeLevel = true;
                anim.SetBool("Level3", GameManager.upgradeLevel);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += 2;
                CurrentLevel++;
                GameManager.CurrentLevelItemUpgrade++;
                GameManager.upgradeLevel = false;
                GameManager.PlayerMaxhealth += 20;
                break;
           case 3:
                GameManager.upgradeLevel = true;
                anim.SetBool("Level4", GameManager.upgradeLevel);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += 2;
                CurrentLevel++;
                GameManager.CurrentLevelItemUpgrade++;
                GameManager.upgradeLevel = false;
                GameManager.PlayerMaxhealth += 20;
                break;
        }

         
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            inRange = true;
            text.text = "Almas Necessarias: " + almas.ToString();
            ItemFala.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            ItemFala.SetActive(false);
        }
    }
}

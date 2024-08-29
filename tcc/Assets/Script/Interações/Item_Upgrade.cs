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
    public int CurrentLevel = 1;
    [Space]
    public string ObjectName;

    private void Start()
    {
        ObjectName = gameObject.name;
        
            switch (ObjectName)
            {
                case "ItemPraUpgrade":
                    switch (GameManager.CurrentLevelItemUpgrade)
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
                    break;

                case "ItemPraUpgrade (1)":
                    switch (GameManager.CurrentLevelItemStaminaUpgrade)
                    {
                        case 1:
                            Debug.Log("Nivel Atual No Start Stamina: " + GameManager.CurrentLevelItemStaminaUpgrade);
                            break;

                        case 2:
                            almas = 6;
                            Debug.Log("Nivel Atual No Start Stamina: " + GameManager.CurrentLevelItemStaminaUpgrade);
                            // anim.SetBool("Level2", true);
                            break;
                        case 3:
                            almas = 8;
                            Debug.Log("Nivel Atual No Start Stamina: " + GameManager.CurrentLevelItemStaminaUpgrade);
                            // anim.SetBool("Level3", true);
                            // anim.SetBool("Level2", false);
                            break;
                    }
                    break;
            }
        
    }



    private void Update()
    {
        if (inRange && Input.GetKey(KeyCode.E))
        {
            if (PlayerMovement.IntemName == ObjectName)
            {
                switch (ObjectName)
                {
                    case "ItemPraUpgrade":
                        Upgrade = GameManager.NumberOfSouls >= almas;
                        break;
                    case "ItemPraUpgrade (1)":
                        Upgrade = GameManager.NumberOfSoulsStamina >= almas;
                        break;
                }
            }
        }

        if (Upgrade) UpgradeItem();
    }

    public void UpgradeItem()
    {
        switch (ObjectName)
        {
            case "ItemPraUpgrade":
                SwitchHealth();
                break;
            case "ItemPraUpgrade (1)":
                SwitchStamina();
                break;
        }
    }

    public void SwitchHealth()
    {
        switch (GameManager.CurrentLevelItemUpgrade)
        {
            case 1:
                Debug.Log("Upgrade Health:" + CurrentLevel);
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
                Debug.Log("Upgrade Health:" + CurrentLevel);
                GameManager.upgradeLevel = true;
                anim.SetBool("Level3", GameManager.upgradeLevel);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += 2;
                CurrentLevel++;
                GameManager.CurrentLevelItemStaminaUpgrade++;
                GameManager.upgradeLevel = false;
                GameManager.PlayerMaxhealth += 20;
                break;
            case 3:
                Debug.Log("Upgrade Health:" + CurrentLevel);
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
    public void SwitchStamina()
    {
        switch (GameManager.CurrentLevelItemStaminaUpgrade)
        {
            case 1:
                Debug.Log("Upgrade stamina:" + CurrentLevel);
                GameManager.UpgradeLevelStamina = true;
                //anim.SetBool("Level2", GameManager.upgradeLevel);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += 2;
                CurrentLevel++;
                GameManager.CurrentLevelItemUpgrade++;
                GameManager.UpgradeLevelStamina = false;
                GameManager.PlayerStamina += 60;
                break;
            case 2:
                Debug.Log("Upgrade stamina:" + CurrentLevel);
                GameManager.UpgradeLevelStamina = true;
              //  anim.SetBool("Level3", GameManager.upgradeLevel);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += 2;
                CurrentLevel++;
                GameManager.CurrentLevelItemUpgrade++;
                GameManager.UpgradeLevelStamina = false;
                GameManager.PlayerMaxhealth += 60;
                break;
            case 3:
                Debug.Log("Upgrade stamina:" + CurrentLevel);
                GameManager.UpgradeLevelStamina = true;
               // anim.SetBool("Level4", GameManager.upgradeLevel);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += 2;
                CurrentLevel++;
                GameManager.CurrentLevelItemUpgrade++;
                GameManager.UpgradeLevelStamina = false;
                GameManager.PlayerMaxhealth += 60;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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

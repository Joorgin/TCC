using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_Upgrade_Stamina : MonoBehaviour
{
    public GameObject ItemFala;
    public TextMeshProUGUI text;
    public int almas;
    public int AddAlmas;
    public Animator anim;
    bool inRange;
    bool Upgrade;
    bool hasUpgraded;
    public int CurrentLevel = 1;
    [Space]
    public string ObjectName;

    private void Start()
    {
        switch (GameManager.CurrentLevelItemStaminaUpgrade)
        {
            case 1:
                break;
            case 2:
                almas = almas + (AddAlmas * GameManager.CurrentLevelItemStaminaUpgrade);
                anim.SetBool("Level2", true);
                break;
            case 3:
                almas = almas + (AddAlmas * GameManager.CurrentLevelItemStaminaUpgrade);
                anim.SetBool("Level3", true);
                break;
        }
    }


    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            Upgrade = GameManager.NumberOfSouls >= almas;
        }

        if (Upgrade) UpgradeItem();
    }

    public void UpgradeItem()
    {
        Debug.Log("ALMAR");
        SwitchStamina();
        hasUpgraded = true;
    }

    public void SwitchStamina()
    {
        switch (GameManager.CurrentLevelItemStaminaUpgrade)
        {
            case 1:
                GameManager.UpgradeLevelStamina = true;
                anim.SetBool("Level2", true);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += AddAlmas;
                CurrentLevel++;
                GameManager.CurrentLevelItemStaminaUpgrade++;
                GameManager.UpgradeLevelStamina = false;
                GameManager.PlayerStamina += 60;
                AtualizarNumeroDeAlmas();
                break;
            case 2:
                GameManager.UpgradeLevelStamina = true;
                anim.SetBool("Level3", true);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += AddAlmas;
                CurrentLevel++;
                GameManager.CurrentLevelItemStaminaUpgrade++;
                GameManager.UpgradeLevelStamina = false;
                GameManager.PlayerStamina += 60;
                AtualizarNumeroDeAlmas();
                break;
            case 3:
                GameManager.UpgradeLevelStamina = true;
                //anim.SetBool("Level4", true);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += AddAlmas;
                CurrentLevel++;
                GameManager.CurrentLevelItemStaminaUpgrade++;
                GameManager.UpgradeLevelStamina = false;
                GameManager.PlayerStamina += 60;
                AtualizarNumeroDeAlmas();
                break;
        }
    }

    void AtualizarNumeroDeAlmas()
    {
        text.text = "Almas Necessarias: " + almas.ToString();
        ItemFala.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            text.text = "Almas Necessarias: " + almas.ToString();
            ItemFala.SetActive(true);
            hasUpgraded = false;
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

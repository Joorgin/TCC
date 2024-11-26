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
    public int AddAlmas;
    public Animator anim;
    bool inRange;
    bool Upgrade;
    public int CurrentLevel = 1;
    [Space]
    public string ObjectName;

    private void Start()
    {
        switch (GameManager.instance.CurrentLevelItemUpgrade)
        {
            case 1:
                Debug.Log("Nivel Atual No Start: " + GameManager.instance.CurrentLevelItemUpgrade);
                break;

            case 2:
                almas = almas + (AddAlmas * GameManager.instance.CurrentLevelItemStaminaUpgrade);
                Debug.Log("Nivel Atual No Start: " + GameManager.instance.CurrentLevelItemUpgrade);
                anim.SetBool("Level2", true);
                break;
            case 3:
                almas = almas + (AddAlmas * GameManager.instance.CurrentLevelItemStaminaUpgrade);
                Debug.Log("Nivel Atual No Start: " + GameManager.instance.CurrentLevelItemUpgrade);
                anim.SetBool("Level3", true);
                break;
            case 4:
                almas = almas + (AddAlmas * GameManager.instance.CurrentLevelItemStaminaUpgrade);
                Debug.Log("Nivel Atual No Start: " + GameManager.instance.CurrentLevelItemUpgrade);
                anim.SetBool("Level4", true);
                break;
            case 5:
                almas = almas + (AddAlmas * GameManager.instance.CurrentLevelItemStaminaUpgrade);
                Debug.Log("Nivel Atual No Start: " + GameManager.instance.CurrentLevelItemUpgrade);
                anim.SetBool("Level5", true);
                break;
            case 6:
                almas = almas + (AddAlmas * GameManager.instance.CurrentLevelItemStaminaUpgrade);
                Debug.Log("Nivel Atual No Start: " + GameManager.instance.CurrentLevelItemUpgrade);
                anim.SetBool("Level6", true);
                break;
        }
    }



    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            Upgrade = GameManager.instance.NumberOfSouls >= almas;
        }

        if (Upgrade) UpgradeItem();
    }

    public void UpgradeItem()
    {
        SwitchHealth();
    }

    public void SwitchHealth()
    {
        switch (GameManager.instance.CurrentLevelItemUpgrade)
        {
            case 1:
                Debug.Log("Upgrade Health:" + CurrentLevel);
                GameManager.instance.upgradeLevel = true;
                anim.SetBool("Level2", GameManager.instance.upgradeLevel);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += AddAlmas;
                CurrentLevel++;
                GameManager.instance.CurrentLevelItemUpgrade++;
                GameManager.instance.upgradeLevel = false;
                GameManager.PlayerMaxhealth += 20;
                break;
            case 2:
                Debug.Log("Upgrade Health:" + CurrentLevel);
                GameManager.instance.upgradeLevel = true;
                anim.SetBool("Level3", GameManager.instance.upgradeLevel);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += AddAlmas;
                CurrentLevel++;
                GameManager.instance.CurrentLevelItemUpgrade++;
                GameManager.instance.upgradeLevel = false;
                GameManager.PlayerMaxhealth += 20;
                break;
            case 3:
                GameManager.instance.upgradeLevel = true;
                anim.SetBool("Level4", GameManager.instance.upgradeLevel);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += AddAlmas;
                CurrentLevel++;
                GameManager.instance.CurrentLevelItemUpgrade++;
                GameManager.instance.upgradeLevel = false;
                GameManager.PlayerMaxhealth += 20;
                break;
            case 4:
                GameManager.instance.upgradeLevel = true;
                anim.SetBool("Level5", GameManager.instance.upgradeLevel);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += AddAlmas;
                CurrentLevel++;
                GameManager.instance.CurrentLevelItemUpgrade++;
                GameManager.instance.upgradeLevel = false;
                GameManager.PlayerMaxhealth += 20;
                break;
            case 5:
                GameManager.instance.upgradeLevel = true;
                anim.SetBool("Level6", GameManager.instance.upgradeLevel);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += AddAlmas;
                CurrentLevel++;
                GameManager.instance.CurrentLevelItemUpgrade++;
                GameManager.instance.upgradeLevel = false;
                GameManager.PlayerMaxhealth += 20;
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

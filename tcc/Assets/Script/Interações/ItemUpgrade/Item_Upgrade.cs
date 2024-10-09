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
        switch (GameManager.CurrentLevelItemUpgrade)
        {
            case 1:
                Debug.Log("Nivel Atual No Start: " + GameManager.CurrentLevelItemUpgrade);
                break;

            case 2:
                almas = almas + (AddAlmas * GameManager.CurrentLevelItemStaminaUpgrade);
                Debug.Log("Nivel Atual No Start: " + GameManager.CurrentLevelItemUpgrade);
                anim.SetBool("Level2", true);
                break;
            case 3:
                almas = almas + (AddAlmas * GameManager.CurrentLevelItemStaminaUpgrade);
                Debug.Log("Nivel Atual No Start: " + GameManager.CurrentLevelItemUpgrade);
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
        SwitchHealth();
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
                almas += AddAlmas;
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
                almas += AddAlmas;
                CurrentLevel++;
                GameManager.CurrentLevelItemUpgrade++;
                GameManager.upgradeLevel = false;
                GameManager.PlayerMaxhealth += 20;
                break;
            case 3:
                Debug.Log("Upgrade Health:" + CurrentLevel);
                GameManager.upgradeLevel = true;
                anim.SetBool("Level4", GameManager.upgradeLevel);
                Contador_de_Almas.instance.DiminiurAlmas(almas);
                Upgrade = false;
                almas += AddAlmas;
                CurrentLevel++;
                GameManager.CurrentLevelItemUpgrade++;
                GameManager.upgradeLevel = false;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemPickable : MonoBehaviour
{
    public float PickUpRadius = 1f;
    public Itens ItemData;
    public int ItemID;

    private CircleCollider2D myCollider;

    bool isInRange;
    InventoryHolder inventory;

    public GameObject DescriptionObject;
    public Image Icon;
    public Sprite IconReference;
    [TextArea(3, 10)]
    public string Description;
    public TextMeshProUGUI descricao;

    private void Awake()
    {
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.isTrigger = true;
        myCollider.radius = PickUpRadius;
    }

    private void Update()
    {
        DescriptionObject.SetActive(isInRange);

        if (isInRange)
        {
            descricao.text = Description;
            Icon.sprite = IconReference;
        }

        if (Input.GetKeyDown(KeyCode.E) && isInRange) AddThing();
    }
    // Verifica se o player entra na range do item
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("InRange");
        if (collision.CompareTag("Player")) isInRange = true;
        inventory = collision.transform.GetComponent<InventoryHolder>();
    }
    // Verifica se o player se mantem na range do item
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("InRange IN Stay"); 
        if (collision.CompareTag("Player")) isInRange = true;
        inventory = collision.transform.GetComponent<InventoryHolder>();
    }
    // Verifica se o player saiu da range do item
    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
    }
    // Adiciona o item no inventário e adiciona seu efeito
    public void AddThing()
    {
        if (!inventory) return;

        if (inventory.InventorySystem.AddToInventory(ItemData, 1))
        {
            switch (ItemID)
            {
                case 0:
                    PlayerAttack.SacrificarKiumbas();
                    break;
                case 1:
                    PlayerHealth.HealthRegen += 2;
                    break;
                case 2:
                    if(!PlayerHealth.canShield) PlayerHealth.canShield = true;
                    if (PlayerHealth.canShield) PlayerHealth.TimeToShieldRemake -= 2;
                    break;
                case 3:
                    PlayerHealth.LibertarKiumbas();
                    break;
                case 4:
                    PlayerHealth.ArmorUP();
                    break;
                case 5:
                    PlayerHealth.SetHabilitStatus();
                    PlayerAttack.setHabilitStatus();
                    TrapSet.SetHabilitStatus();
                    break;
                case 6:
                    PlayerHealth.SetMirror();
                    break;
                case 7:
                    PlayerAttack.SetCritChance();
                    break;
                case 8:
                    PlayerHealth.ChanceOfPatua();
                    break;
                case 9:
                    PlayerMovement.AddChanceOfAChest();
                    break;
            }
            Debug.Log("Adicionou O Item");
            Destroy(this.gameObject);
        }
    }
}

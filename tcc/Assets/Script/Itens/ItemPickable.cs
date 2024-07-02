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

    private void FixedUpdate()
    {
        DescriptionObject.SetActive(isInRange);

        if (isInRange)
        {
            descricao.text = Description;
            Icon.sprite = IconReference;
        }

        if (Input.GetKeyDown(KeyCode.E) && isInRange) AddThing();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("InRange");
        if (collision.CompareTag("Player")) isInRange = true;
        inventory = collision.transform.GetComponent<InventoryHolder>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("InRange IN Stay"); 
        if (collision.CompareTag("Player")) isInRange = true;
        inventory = collision.transform.GetComponent<InventoryHolder>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
    }

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
            }
            Destroy(this.gameObject);
        }
    }
}

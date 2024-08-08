using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlot_UI[] slots;
    //SpawnChest

    protected override void Start()
    {
        base.Start();

        inventoryHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryHolder>();

        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.InventorySystem;
            inventorySystem.OnInventorySlotChanged += updateSlot;
        }
        else Debug.LogWarning($"No inventory assigned to {this.gameObject}");

        AssignSlot(inventorySystem);
    }

    protected override void Update()
    {
        base.Update();

        if (inventoryHolder == null) 
        {
            inventoryHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryHolder>();
        }
    }
    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (slots.Length != inventorySystem.inventorySize) Debug.Log($"Inventory slots out of sync in {this.gameObject}");

        for(int i =0; i < inventorySystem.inventorySize; i++)
        {
            slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }

   
}
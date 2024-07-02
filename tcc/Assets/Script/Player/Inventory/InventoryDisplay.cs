using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;

    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void updateSlot(InventorySlot updatedSlot)
    {
        foreach(var slot in SlotDictionary)
        {
            if (slot.Value == updatedSlot)
                slot.Key.UpdateUISlot(updatedSlot);
        }
    }


}

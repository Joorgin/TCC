using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;


[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;
    public List<InventorySlot> InventorySlots => inventorySlots;

    public int inventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i< size; i++)
        {
            InventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(Itens itemToAdd, int amaountToAdd)
    {
        if(ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) // v� se o item existe no inventario
        {
            foreach (var slot in invSlot)
            {
                if(slot.RoomLeftInStack(amaountToAdd))
                {
                    slot.AddToStack(amaountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
            
        }
        
        if(HasFreeSlot(out InventorySlot freeSlot)) // pega o primeiro slot livre
        {
           freeSlot.UpdateInventorySlot(itemToAdd, amaountToAdd);  
           OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        return false;
    }

    public bool ContainsItem(Itens ItemToAdd, out List<InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(i => i.itemData == ItemToAdd).ToList();
        return invSlot == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot FreeSlot) 
    {
        FreeSlot = inventorySlots.FirstOrDefault(i => i.itemData == null);
        return FreeSlot == null ? false : true;
    }

}

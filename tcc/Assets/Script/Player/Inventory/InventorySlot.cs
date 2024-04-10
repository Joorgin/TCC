using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class InventorySlot 
{
    [SerializeField] private Itens ItemData;
    [SerializeField] private int StackSize;

    public Itens itemData => ItemData;
    public int stackSize => StackSize;
    
    public void UpdateInventorySlot(Itens data, int amount)
    {
        ItemData = data;
        StackSize = amount; 
    }

    public InventorySlot(Itens source, int amount)
    {
        ItemData = source;
        StackSize = amount;
    }

    public InventorySlot() 
    { 
       ItemData = null;
       StackSize = -1;
    }

    public void AddToStack(int amount) 
    {
        StackSize += amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemainig) 
    {
        amountRemainig = itemData.MaxStackSise - stackSize;
        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if (StackSize + amountToAdd <= ItemData.MaxStackSise) return true;
        else return false;
    }
}

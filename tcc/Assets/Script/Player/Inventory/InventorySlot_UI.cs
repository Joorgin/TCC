using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemsprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedInventorySlot;

    public InventorySlot AssignedInventorySlot => assignedInventorySlot;

    public InventoryDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        itemsprite.sprite = null;
        itemsprite.color = Color.clear;
        itemCount.text = "";

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

   

    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot;
        UpdateUISlot(slot);
    }

    

    public void UpdateUISlot(InventorySlot slot)
    {
        if(slot.itemData !=null)
        {
            itemsprite.sprite = slot.itemData.Icon;
            itemsprite.color = Color.white;

            if (slot.stackSize > 1) itemCount.text = slot.stackSize.ToString();
            else itemCount.text = "";
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        assignedInventorySlot?.ClearSlot();
        itemsprite.sprite = null;
        itemsprite.color= Color.clear;
        itemCount.text= "";
    }

    public void UpdateUISlot()
    {
        if (assignedInventorySlot != null) UpdateUISlot(assignedInventorySlot);
    }
}

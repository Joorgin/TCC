using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemsprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedInventorySlot;
    [SerializeField] private TextMeshProUGUI Description;
    public string nameButton;
    public InventorySlot AssignedInventorySlot => assignedInventorySlot;

    public InventoryDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        itemsprite.sprite = null;
        itemsprite.color = Color.clear;
        itemCount.text = "";

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            GameObject gameObjectUnderMouse = null;

            // Check each hit to see if it's a UI element
            foreach (RaycastResult result in raycastResults)
            {
                if (result.gameObject.GetComponent<Image>() != null)
                {
                    gameObjectUnderMouse = result.gameObject;
                    Debug.Log(gameObjectUnderMouse);
                    break;
                }
            }


            if (gameObjectUnderMouse != null)
            {
                if (gameObjectUnderMouse.name == nameButton)
                {
                    Description.gameObject.SetActive(true);
                }
                else
                {
                    Description.gameObject.SetActive(false);    
                }
            }
        }
        else
        {
            Description.gameObject.SetActive(false);
        }
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
            Description.text = slot.itemData.Description;

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
